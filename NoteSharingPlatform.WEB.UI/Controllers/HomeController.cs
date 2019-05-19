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
        private NoteManager noteMan = new NoteManager();
        // GET: Home
        public ActionResult Index()
        {
            
            return View(noteMan.ListQueryable().OrderByDescending(x=>x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {
            
            return View("Index",noteMan.ListQueryable().OrderByDescending(x=>x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

       
    }

   
}