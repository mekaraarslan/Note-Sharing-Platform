using NoteSharingPlatform.COMMON;
using NoteSharingPlatform.ENTITY.Models;
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
            if (HttpContext.Current.Session["login"] != null)
            {
                UserModel user = HttpContext.Current.Session["login"] as UserModel;
                return user.Username;
            }
            return "system";
        }
    }
}