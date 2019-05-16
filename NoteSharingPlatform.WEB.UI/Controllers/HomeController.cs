using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteSharingPlatform.WEB.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (TempData["note"] != null)
            {
                return View((TempData["note"] as List<Note>).OrderByDescending(x=>x.ModifiedOn).ToList());
            }

            NoteManager noteMan = new NoteManager();
            return View(noteMan.GetAllNote().OrderByDescending(x=>x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {
            NoteManager noteMan = new NoteManager();
            
            return View("Index",noteMan.GetAllNote().OrderByDescending(x=>x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

       
    }

   
}