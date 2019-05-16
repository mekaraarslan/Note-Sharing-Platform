using NoteSharingPlatform.DAL.EntityFramework;
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
                    userResult.Errors.Add("Bu kullanıcı adı kayıtlı !!!");
                }
                if (user.Email == registerViewModel.Email)
                {
                    userResult.Errors.Add("Bu e-posta adresi kayıtlı !!!");
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
    }
}
