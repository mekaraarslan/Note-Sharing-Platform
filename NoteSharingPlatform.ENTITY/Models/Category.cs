using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.ENTITY.Models
{
    public class Category  : MyEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //public DateTime CreatedOn { get; set; }
        //public DateTime ModifiedOn { get; set; }
        //public string ModifiedUserName { get; set; }

        public virtual List<Note> Notes { get; set; }

        public Category()
        {
            Notes = new List<Note>();
        }
    }
}
