using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace NoteSharingPlatform.WEB.UI.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoriesFromCache()
        {
            var result = WebCache.Get("category-cache");

            if (result == null)
            {
                CategoryManager categoryMan = new CategoryManager();
                result = categoryMan.List();
                WebCache.Set("category-cache", result, 20, true); // Cache ismi , cache atılacak değer , ne kadar süre cachede kalacak , her çağırdımızda süre sıfırlansın mı
            }
            return result;
             
        }

        public static void RemoveCategoriesFromCache()
        {
            RemoveCache("category-cache");
        }

        public static void RemoveCache(string key)
        {
            WebCache.Remove(key);
        }
    }
}