using NoteSharingPlatform.BLL;
using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.ENTITY.Models;
using NoteSharingPlatform.ENTITY.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteSharingPlatform.WEB.UI.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                UserManager userMan = new UserManager();
                BusinessLayerResult<UserModel> userResult = userMan.RegisterUser(registerViewModel);

                if (userResult.Errors.Count > 0)
                {
                    userResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(registerViewModel);
                }



                return RedirectToAction("RegisterOK");
            }

            
            
            return View();
        }

        public ActionResult RegisterOK()
        {
            return View();
        }
    }
}