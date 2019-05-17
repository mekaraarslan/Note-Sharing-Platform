using NoteSharingPlatform.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteSharingPlatform.WEB.UI.Controllers
{
    public class UserProfileController : Controller
    {
        [HttpGet]
        public ActionResult ShowProfile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(UserModel user)
        {
            return View();
        }

        [HttpGet]
        public ActionResult RemoveProfile()
        {
            return View();
        }

    }
}