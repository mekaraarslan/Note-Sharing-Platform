using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.BLL.Results;
using NoteSharingPlatform.ENTITY.Models;


namespace NoteSharingPlatform.WEB.UI.Controllers
{
    public class UserModelController : Controller
    {

        private UserManager userMan = new UserManager();

        public ActionResult Index()
        {
            return View(userMan.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = userMan.Find(x=>x.Id == id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.Password = Crypto.SHA256(userModel.Password);
                BusinessLayerResult<UserModel> result = userMan.Insert(userModel);

                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(userModel);
                }
                return RedirectToAction("Index");
            }
            return View(userModel);
        }

 
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = userMan.Find(x => x.Id == id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<UserModel> result = userMan.Update(userModel);

                if (result.Errors.Count>0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(userModel);
                }

                return RedirectToAction("Index");
            }
            return View(userModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = userMan.Find(x => x.Id == id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel userModel = userMan.Find(x => x.Id == id);
            userMan.Delete(userModel);

            return RedirectToAction("Index");
        }

    
    }
}
