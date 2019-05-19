using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteSharingPlatform.WEB.UI.ViewModels.NotifyViewModels
{
    public class WarningViewModel : NotifyViewModelBase<string>
    {
        public WarningViewModel()
        {
            Title = "Uarı !";
        }
    }
}