using NoteSharingPlatform.DAL.EntityFramework;
using NoteSharingPlatform.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.BLL.Managers
{
    public class CategoryManager
    {
        private Repository<Category> categoryRep = new Repository<Category>();

        public List<Category> GetCategories()
        {
            return categoryRep.List();
        }

        public Category GetCategoryById(int id)
        {
            return categoryRep.Find(x => x.Id == id);
        }

     
    }
}
