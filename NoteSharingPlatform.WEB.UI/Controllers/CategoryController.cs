using NoteSharingPlatform.BLL.Managers;
using NoteSharingPlatform.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NoteSharingPlatform.WEB.UI.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Select(int? id)
        {
            if (id == null)
            {
                // Id boş ise bir hata mesajı çıkartıyoruz.
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager categoryMan = new CategoryManager();
            Category category = categoryMan.GetCategoryById(id.Value); // Null olabilir dediğimiz için herhangi bir değer vermek gerekir.

            if (category == null)
            {
                return HttpNotFound();
            }

            TempData["note"] = category.Notes;
            return RedirectToAction("Index", "Home");
        }

       
    }
}