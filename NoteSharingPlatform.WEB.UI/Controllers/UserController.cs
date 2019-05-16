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
            Session.Clear();
            return RedirectToAction("Index", "Home");
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

        public ActionResult UserActivate(Guid Id)
        {
            UserManager userMan = new UserManager();
            BusinessLayerResult<UserModel> result = userMan.ActivateUser(Id);

            if (result.Errors.Count > 0)
            {
                TempData["errors"] = result.Errors;
                return RedirectToAction("UserActivateCancel");
            }

            return RedirectToAction("UserActivateOK");
        }

        public ActionResult UserActivateOK()
        {
            return View();
        }

        public ActionResult UserActivateCancel()
        {
            List<ErrorMessageObject> errors = null;
            if (TempData["errors"] != null)
            {
                errors = TempData["errors"] as List<ErrorMessageObject>;
            }
            return View(errors);
        }
    }
}