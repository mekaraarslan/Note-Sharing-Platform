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
    public class UserManager
    {
        private Repository<UserModel> userRep = new Repository<UserModel>();

        public BusinessLayerResult<UserModel> RegisterUser(RegisterViewModel registerViewModel)
        {
            UserModel user = userRep.Find(x => x.Username == registerViewModel.Username || x.Email == registerViewModel.Email);
            BusinessLayerResult<UserModel> userResult = new BusinessLayerResult<UserModel>();

            if (user != null)
            {
                if (user.Username == registerViewModel.Username)
                {
                    userResult.AddError(ErrorMessageCode.UsernameAlreadyExists,"Bu kullanıcı adı kayıtlı !!!");
                }
                if (user.Email == registerViewModel.Email)
                {
                    userResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu e-posta adresi kayıtlı !!!");
                   
                }
            }
            else
            {
                int dbResult = userRep.Insert(new UserModel()
                {
                    Username = registerViewModel.Username,
                    Email = registerViewModel.Email,
                    Password = registerViewModel.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false

                });

                if (dbResult > 0)
                {
                    userResult.Result = userRep.Find(x => x.Email == registerViewModel.Email && x.Username == registerViewModel.Username);

                }
            }

            return userResult;
        }

        public BusinessLayerResult<UserModel> LoginUser (LoginViewModel loginViewModel)
        {
            //Giriş kontrolü
            //Hesap aktive edilmiş mi?
            BusinessLayerResult<UserModel> userResult = new BusinessLayerResult<UserModel>();
            userResult.Result = userRep.Find(x => x.Username == loginViewModel.Username && x.Password == loginViewModel.Password);

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
    }
}
