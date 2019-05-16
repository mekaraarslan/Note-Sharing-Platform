using NoteSharingPlatform.ENTITY.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.DAL.EntityFramework
{
    public class NSPContext : DbContext
    {
        public NSPContext()
        {
            Database.SetInitializer<NSPContext>(new MyInitializer());
        }

        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }


        protected override void OnModelCreating(DbModelBuilder mb)
        {

            #region Table Settings

           
            mb.Entity<Comment>().HasKey(x => x.Id);
            mb.Entity<Category>().HasKey(x => x.Id);
            mb.Entity<Note>().HasKey(x => x.Id);
            mb.Entity<UserModel>().HasKey(x => x.Id);

            mb.Entity<Liked>().HasKey(x => x.Id);
            mb.Entity<Liked>().ToTable("Likes");
            
            #endregion

            #region Column Settings

            // Category Column Settings
            mb.Entity<Category>().Property(x => x.Title).HasMaxLength(50).IsRequired();
            mb.Entity<Category>().Property(x => x.Description).HasMaxLength(200);


            // Comment Column Settings
            mb.Entity<Comment>().Property(x => x.Text).HasMaxLength(300).IsRequired();


            // UserModel Column Settings
            mb.Entity<UserModel>().Property(x => x.Name).HasMaxLength(40);
            mb.Entity<UserModel>().Property(x => x.Surname).HasMaxLength(40);
            mb.Entity<UserModel>().Property(x => x.Username).HasMaxLength(50).IsRequired();
            mb.Entity<UserModel>().Property(x => x.Email).HasMaxLength(80).IsRequired();
            mb.Entity<UserModel>().Property(x => x.Password).HasMaxLength(250).IsRequired();
            mb.Entity<UserModel>().Property(x => x.ActivateGuid).IsRequired();


            // Note Column Settings
            mb.Entity<Note>().Property(x => x.Title).HasMaxLength(100).IsRequired();
            mb.Entity<Note>().Property(x => x.Text).HasMaxLength(3000).IsRequired();


            #endregion



            base.OnModelCreating(mb);
        }


    }
}
