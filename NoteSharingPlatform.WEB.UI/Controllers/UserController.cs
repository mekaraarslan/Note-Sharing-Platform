using NoteSharingPlatform.BLL;
using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.ENTITY.Messages;
using NoteSharingPlatform.ENTITY.Models;
using NoteSharingPlatform.ENTITY.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            if (ModelState.IsValid)
            {
                UserManager userMan = new UserManager();
                loginViewModel.Password = Crypto.SHA256(loginViewModel.Password);
                BusinessLayerResult<UserModel> userResult = userMan.LoginUser(loginViewModel);
                loginViewModel.Password = string.Empty; // Kullanıcı şifre güvenliği için şifre sessionda tutulmamalıdır.Bu yüzden password değerini sıfırlıyoruz.
                if (userResult.Errors.Count > 0)
                {
                    userResult.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(loginViewModel);
                }

                Session["login"] = userResult.Result;
                return RedirectToAction("Index", "Home");
 
            }

            return View(loginViewModel);
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
                registerViewModel.Password = Crypto.SHA256(registerViewModel.Password);
                BusinessLayerResult<UserModel> userResult = userMan.RegisterUser(registerViewModel);

                if (userResult.Errors.Count > 0)
                {
                    userResult.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
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