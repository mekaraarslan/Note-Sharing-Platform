using NoteSharingPlatform.BLL.Abstract;
using NoteSharingPlatform.BLL.Results;
using NoteSharingPlatform.COMMON.Helpers;
using NoteSharingPlatform.DAL.EntityFramework;
using NoteSharingPlatform.ENTITY.Messages;
using NoteSharingPlatform.ENTITY.Models;
using NoteSharingPlatform.ENTITY.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NoteSharingPlatform.BLL.Managers
{
    public class UserManager : ManagerBase<UserModel>
    {

        public BusinessLayerResult<UserModel> RegisterUser(RegisterViewModel registerViewModel)
        {
            UserModel user = Find(x => x.Username == registerViewModel.Username || x.Email == registerViewModel.Email);
            BusinessLayerResult<UserModel> userResult = new BusinessLayerResult<UserModel>();

            if (user != null)
            {
                if (user.Username == registerViewModel.Username)
                {
                    userResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı kayıtlı !!!");
                }
                if (user.Email == registerViewModel.Email)
                {
                    userResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu e-posta adresi kayıtlı !!!");

                }
            }
            else
            {
                int dbResult = base.Insert(new UserModel()
                {
                    Username = registerViewModel.Username,
                    Email = registerViewModel.Email,
                    ProfileImageFileName = "DefaultProfileImage.png",
                    Password = registerViewModel.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false

                });

                if (dbResult > 0)
                {
                    userResult.Result = Find(x => x.Email == registerViewModel.Email && x.Username == registerViewModel.Username);
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/User/UserActivate/{userResult.Result.ActivateGuid}";
                    string body = $"Merhaba {userResult.Result.Username} ; <br/><br/>Hesabınızı aktifleştirmek için <a href = '{activateUri}' target =_blank'> tıklayınız. </a>";

                    MailHelper.SendMail(body, userResult.Result.Email, "Note Sharing Platform Hesap Aktifleştirme");

                }
            }

            return userResult;
        }

        public BusinessLayerResult<UserModel> GetUserById(int id)
        {
            BusinessLayerResult<UserModel> res = new BusinessLayerResult<UserModel>();
            res.Result = Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<UserModel> LoginUser(LoginViewModel loginViewModel)
        {
            //Giriş kontrolü
            //Hesap aktive edilmiş mi?
            BusinessLayerResult<UserModel> userResult = new BusinessLayerResult<UserModel>();
            userResult.Result = Find(x => x.Username == loginViewModel.Username && x.Password == loginViewModel.Password);

            if (userResult.Result != null)
            {
                if (!userResult.Result.IsActive)
                {
                    userResult.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    userResult.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");
                }

            }
            else
            {
                userResult.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı veya şifre doğru değil.");
            }


            return userResult;

        }

        public BusinessLayerResult<UserModel> ActivateUser(Guid activateId)
        {
            BusinessLayerResult<UserModel> userResult = new BusinessLayerResult<UserModel>();
            userResult.Result = Find(x => x.ActivateGuid == activateId);

            if (userResult != null)
            {
                if (userResult.Result.IsActive)
                {
                    userResult.AddError(ErrorMessageCode.UserAlreadyActivate, "Kullanıcı zaten aktif edilmiştir.");
                    return userResult;
                }
                userResult.Result.IsActive = true;
                Update(userResult.Result);
            }
            else
            {
                userResult.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanılıcı bulunamadı.");

            }

            return userResult;
        }

        public BusinessLayerResult<UserModel> UpdateProfile(UserModel model)
        {
            UserModel user = Find(x=>x.Id != model.Id && (x.Username == model.Username || x.Email == model.Email));
            BusinessLayerResult<UserModel> result = new BusinessLayerResult<UserModel>();

            if (user != null && user.Id != model.Id)
            {
                if (user.Username == model.Username)
                {
                    result.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı zaten kayıtlı");
                }
                if (user.Email == model.Email)
                {
                    result.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu mail adresi zaten kayıtlı");

                }
                return result;
            }

            result.Result = Find(x => x.Id == model.Id);
            result.Result.Name = model.Name;
            result.Result.Surname = model.Surname;
            result.Result.Username = model.Username;
            result.Result.Email = model.Email;
            result.Result.Password = model.Password;

            if (string.IsNullOrEmpty(model.ProfileImageFileName) == false)
            {
                result.Result.ProfileImageFileName = model.ProfileImageFileName;
            }
            if (base.Update(result.Result) == 0)
            {
                result.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil Güncellenemedi.");
            }

            return result;
        }

        public BusinessLayerResult<UserModel> RemoveUserById(int id)
        {
            BusinessLayerResult<UserModel> result = new BusinessLayerResult<UserModel>();
            UserModel user = Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) == 0 )
                {
                    result.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi.");
                    return result;
                }
            }
            else
            {
                result.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
            }
            return result;
        }

        // Method gizleme işlemi yaptık
        public new BusinessLayerResult<UserModel> Insert(UserModel userModel)
        {
            UserModel user = Find(x => x.Username == userModel.Username || x.Email == userModel.Email);
            BusinessLayerResult<UserModel> userResult = new BusinessLayerResult<UserModel>();

            userResult.Result = userModel;

            if (user != null)
            {
                if (user.Username == userModel.Username)
                {
                    userResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı kayıtlı !!!");
                }
                if (user.Email == userModel.Email)
                {
                    userResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu e-posta adresi kayıtlı !!!");

                }
            }
            else
            {
                userModel.ProfileImageFileName = "DefaultProfileImage.png";
                userModel.ActivateGuid = Guid.NewGuid();

                if(base.Insert(userResult.Result) == 0)
                {
                    userResult.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı Eklenemedi.");
                }

               
            }

            return userResult;
        }

        // Method gizleme işlemi yaptık
        public new BusinessLayerResult<UserModel> Update(UserModel model)
        {
            UserModel user = Find(x => x.Id != model.Id && (x.Username == model.Username || x.Email == model.Email));
            BusinessLayerResult<UserModel> result = new BusinessLayerResult<UserModel>();
            result.Result = model;

            if (user != null && user.Id != model.Id)
            {
                if (user.Username == model.Username)
                {
                    result.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı zaten kayıtlı");
                }
                if (user.Email == model.Email)
                {
                    result.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu mail adresi zaten kayıtlı");

                }
                return result;
            }

            result.Result = Find(x => x.Id == model.Id);
            result.Result.Name = model.Name;
            result.Result.Surname = model.Surname;
            result.Result.Username = model.Username;
            result.Result.Email = model.Email;
            result.Result.Password = model.Password;
            result.Result.IsActive = model.IsActive;
            result.Result.IsAdmin = model.IsAdmin;


            
            if (base.Update(result.Result) == 0)
            {
                result.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı Güncellenemedi.");
            }

            return result;
        }

    }
}
