using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NoteSharingPlatform.ENTITY.Models;

namespace NoteSharingPlatform.DAL.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<NSPContext>
    {
        protected override void Seed(NSPContext context)
        {

            // Adding admin user
            UserModel admin = new UserModel()
            {
                Name = "Muhammed Emin",
                Surname = "Karaarslan",
                Email = "muhammed.e.karaarslan@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "mekaraarslan",
                Password = "10432",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "mekaraarslan"
            };

            // Adding standart user
            UserModel standartUser = new UserModel()
            {
                Name = "Emirhan",
                Surname = "Karaarslan",
                Email = "emirkaraarsln@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "emirhanka",
                Password = "123456",
                CreatedOn = DateTime.Now.AddHours(5),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUserName = "mekaraarslan"
            };

            context.UserModels.Add(admin);
            context.UserModels.Add(standartUser);

            // Adding fake users

            for (int i = 0; i < 10; i++)
            {
                UserModel fakeUser = new UserModel()
                {

                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetFirstName(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i * i * i - i * 9}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user{i * i * i - i * 9}"
                };
                context.UserModels.Add(fakeUser);
            }

            context.SaveChanges();

            // User list for using
            List<UserModel> userList = context.UserModels.ToList();

            // Adding fake categories

            for (int i = 0; i < 10; i++)
            {
                Category category = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "mekaraarslan"
                };

                context.Categories.Add(category);

                // Adding fake notes
                for (int j = 0; j < FakeData.NumberData.GetNumber(5, 9); j++)
                {
                    UserModel note_owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = note_owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = note_owner.Username
                    };

                    category.Notes.Add(note);

                    // Adding fake comments

                    for (int y = 0; y < FakeData.NumberData.GetNumber(3, 5); y++)
                    {
                        UserModel comment_owner = userList[FakeData.NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            UserModel = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName = comment_owner.Username,

                        };

                        note.Comments.Add(comment);
                    }

                    // Adding fake likes
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            UserModel = userList[m]
                        };
                        note.Likes.Add(liked);
                    }


                }


            }

            context.SaveChanges();
        }
    }
}
