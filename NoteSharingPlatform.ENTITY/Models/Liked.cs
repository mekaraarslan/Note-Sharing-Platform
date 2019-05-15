using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.ENTITY.Models
{
    public class Liked
    {
        public int Id { get; set; }

        public virtual Note Note { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}
