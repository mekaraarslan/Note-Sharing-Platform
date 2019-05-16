﻿using NoteSharingPlatform.COMMON;
using NoteSharingPlatform.WEB.UI.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NoteSharingPlatform.WEB.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            App.common = new WebCommon();
        }
    }
}
