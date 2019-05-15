using NoteSharingPlatform.DAL.EntityFramework;
using NoteSharingPlatform.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.BLL.Managers
{
    public class NoteManager
    {
        private Repository<Note> noteRep = new Repository<Note>();
        public List<Note> GetAllNote()
        {
            return noteRep.List();
        }

    }
}
