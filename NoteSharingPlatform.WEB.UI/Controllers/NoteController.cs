using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.ENTITY.Models;
using NoteSharingPlatform.WEB.UI.Models;

namespace NoteSharingPlatform.WEB.UI.Controllers
{
    public class NoteController : Controller
    {
        private NoteManager noteMan = new NoteManager();
        private CategoryManager categoryMan = new CategoryManager();
        private LikedManager likedMan = new LikedManager();

        public ActionResult Index()
        {
            //Include =  Bir nevi join işlemi yapıyoruz parametre olarak verilen değerler navigation propertilerinin ismi
            var notes = noteMan.ListQueryable().Include("Category").Include("Owner").Where(x => x.Owner.Id == CurrentSession.User.Id).OrderByDescending(x => x.ModifiedOn);
            return View(notes.ToList());
        }

        public ActionResult MyLikedNotes()
        {
            var notes = likedMan.ListQueryable().Include("UserModel").Include("Note").Where(x => x.UserModel.Id == CurrentSession.User.Id).Select(x => x.Note).Include("Category").Include("Owner").OrderByDescending(x => x.ModifiedOn);

            return View("Index",notes.ToList());
        }
    

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteMan.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                note.Owner = CurrentSession.User;
                noteMan.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteMan.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                Note dbNote = noteMan.Find(x => x.Id == note.Id);
                dbNote.IsDraft = note.IsDraft;
                dbNote.CategoryId = note.CategoryId
;               dbNote.Text = note.Text;
                dbNote.Title = note.Title;

                noteMan.Update(dbNote);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteMan.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteMan.Find(x => x.Id == id);
            noteMan.Delete(note);

            return RedirectToAction("Index");
        }

       
    }
}
