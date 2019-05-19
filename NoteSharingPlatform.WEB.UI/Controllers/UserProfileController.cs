using NoteSharingPlatform.BLL;
using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.ENTITY.Messages;
using NoteSharingPlatform.ENTITY.Models;
using NoteSharingPlatform.WEB.UI.ViewModels.NotifyViewModels;
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
            UserModel currentUser = Session["login"] as UserModel;
            UserManager userMan = new UserManager();
            BusinessLayerResult<UserModel> res = userMan.GetUserById(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObject = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    RedirectingTimeout = 4000,
                    Items = res.Errors

                };

                return View("ErrorView", errorNotifyObject);
            }

            return View(res.Result);
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