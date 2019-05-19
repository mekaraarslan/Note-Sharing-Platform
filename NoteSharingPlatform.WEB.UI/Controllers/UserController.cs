using NoteSharingPlatform.BLL;
using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.BLL.Results;
using NoteSharingPlatform.ENTITY.Messages;
using NoteSharingPlatform.ENTITY.Models;
using NoteSharingPlatform.ENTITY.ViewModels;
using NoteSharingPlatform.WEB.UI.ViewModels.NotifyViewModels;
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
        private UserManager userMan = new UserManager();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
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

                registerViewModel.Password = Crypto.SHA256(registerViewModel.Password);
                BusinessLayerResult<UserModel> userResult = userMan.RegisterUser(registerViewModel);

                if (userResult.Errors.Count > 0)
                {
                    userResult.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(registerViewModel);
                }
                OKViewModel notifyObject = new OKViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Index",
                };
                notifyObject.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon linkine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden not ekleyemez ve beğenme yapamazsınız.");

                return View("OKView", notifyObject);
            }



            return View();
        }

        public ActionResult UserActivate(Guid Id)
        {
            
            BusinessLayerResult<UserModel> result = userMan.ActivateUser(Id);

            if (result.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObject = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = result.Errors
                };

                return View("ErrorView", ErrorNotifyObject);
            }

            OKViewModel OKNotifyObject = new OKViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/User/Login",

            };
            OKNotifyObject.Items.Add("Hesabınız aktifleştirildi. Artık not paylaşabilir ve  beğenme yapabilirsiniz.");
            return View("OKView", OKNotifyObject);
        }

    }
}