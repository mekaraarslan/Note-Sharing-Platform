using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.ENTITY.Models
{
    public class Comment  : MyEntityBase
    {
        public int Id { get; set; }
        public string Text { get; set; }

        //public DateTime CreatedOn { get; set; }
        //public DateTime ModifiedOn { get; set; }
        //public string ModifiedUserName { get; set; }

        public virtual Note Note { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}
