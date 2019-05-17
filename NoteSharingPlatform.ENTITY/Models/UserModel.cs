﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.ENTITY.Models
{
    public class UserModel : MyEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImageFileName { get; set; }


        public bool IsActive { get; set; }
        public Guid ActivateGuid { get; set; }
        public bool IsAdmin { get; set; }

        //public DateTime CreatedOn { get; set; }
        //public DateTime ModifiedOn { get; set; }
        //public string ModifiedUserName { get; set; }

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }

    }
}
