using NoteSharingPlatform.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.DAL.EntityFramework
{
    public  class RepositoryBase
    {
        protected static NSPContext db;
        private static object _lockSync = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }

        private static void CreateContext()
        {
            if (db == null)
            {
                // Çoklu işlem durumlarında önlem almak için kullanılır. lock metodu sadece tek bir işlem tarafından kullanılabilir.İşlem bitene kadar başka bir işlemin başlatılmasına izin vermez.
                lock (_lockSync)
                {
                    if (db == null)
                    {
                        db = new NSPContext();
                    }
                }
                db = new NSPContext();
            }
        }

       
    }
}
