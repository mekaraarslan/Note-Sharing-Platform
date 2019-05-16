using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoteSharingPlatform.ENTITY.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez.") , StringLength(50 , ErrorMessage ="{0} adı maksimum {1} karakter olmalı.")]
        public string Username { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(80, ErrorMessage = "{0} maksimum {1} karakter olmalı.") , EmailAddress(ErrorMessage ="{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password) , StringLength(40, ErrorMessage = "{0} maksimum {1} karakter olmalı.")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password) , StringLength(40, ErrorMessage = "{0} maksimum {1} karakter olmalı."),Compare("Password" , ErrorMessage ="{0} ile {1} uyuşmuyor !!!")]
        public string RePassword { get; set; }
    }
}