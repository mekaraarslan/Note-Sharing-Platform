using NoteSharingPlatform.COMMON;
using NoteSharingPlatform.ENTITY.Models;
using NoteSharingPlatform.WEB.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteSharingPlatform.WEB.UI.Init
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            UserModel user = CurrentSession.User;
            if (user != null)
                return user.Username;

            else
                return "system";
        }
    }
}