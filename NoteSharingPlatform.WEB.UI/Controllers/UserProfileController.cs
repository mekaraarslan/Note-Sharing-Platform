using NoteSharingPlatform.BLL;
using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.BLL.Results;
using NoteSharingPlatform.ENTITY.Messages;
using NoteSharingPlatform.ENTITY.Models;
using NoteSharingPlatform.WEB.UI.Models;
using NoteSharingPlatform.WEB.UI.ViewModels.NotifyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NoteSharingPlatform.WEB.UI.Controllers
{
    public class UserProfileController : Controller
    {
        private UserManager userMan = new UserManager();

        [HttpGet]
        public ActionResult ShowProfile()
        {
            BusinessLayerResult<UserModel> res = userMan.GetUserById(CurrentSession.User.Id);

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
            return View(CurrentSession.User);
        }

        [HttpPost]
        public ActionResult EditProfile(UserModel user , HttpPostedFileBase profileImage)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (profileImage != null &&
                (profileImage.ContentType == "image/jpeg" ||
                profileImage.ContentType == "image/jpg" ||
                profileImage.ContentType == "image/png"))
                {
                    string fileName = $"userProfileImage_{user.Id}.{profileImage.ContentType.Split('/')[1]}";
                    profileImage.SaveAs(Server.MapPath($"~/Images/{fileName}"));
                    user.ProfileImageFileName = fileName;
                }

                //user.Password = Crypto.SHA256(user.Password);
                BusinessLayerResult<UserModel> result = userMan.UpdateProfile(user);

                if (result.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObject = new ErrorViewModel()
                    {
                        Items = result.Errors,
                        Title = "Profil Güncellenemedi !!!",
                        RedirectingUrl = "/UserProfile/EditProfile",
                        RedirectingTimeout = 3000
                    };
                    return View("ErrorView", errorNotifyObject);
                }

                CurrentSession.Set<UserModel>("login", result.Result);  // Profil güncellendiği için sessionda güncellendi .
                return RedirectToAction("ShowProfile");

            }
            return View(user);
        }

        [HttpGet]
        public ActionResult RemoveProfile()
        {
           
            
            BusinessLayerResult<UserModel> result = userMan.RemoveUserById(CurrentSession.User.Id);

            if (result.Errors.Count>0)
            {
                ErrorViewModel errorNotifyObject= new ErrorViewModel()
                {
                    Items = result.Errors,
                    Title = "Profil Silinemedi.",
                    RedirectingUrl = "/UserProfile/ShowProfile",
                    RedirectingTimeout = 3000
                };
                return View("ErrorView", errorNotifyObject);
            }
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}