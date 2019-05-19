using NoteSharingPlatform.ENTITY.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteSharingPlatform.WEB.UI.ViewModels.NotifyViewModels
{
    public class OKViewModel : NotifyViewModelBase<string>
    {
        public OKViewModel()
        {
            Title = "İşlem Başarılı";
        }
    }
}