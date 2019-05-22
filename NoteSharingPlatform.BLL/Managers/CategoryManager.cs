using NoteSharingPlatform.BLL.Abstract;
using NoteSharingPlatform.ENTITY.Models;
using System.Linq;

namespace NoteSharingPlatform.BLL.Managers
{
    public class CategoryManager : ManagerBase<Category>
    {
        public override int Delete(Category obj)
        {
            NoteManager noteMan = new NoteManager();
            LikedManager likedMan = new LikedManager();
            //CommentManager commentMan = new CommentManager();

            // Kategori ile ilişkili notların silinmesi gerekiyor.
            foreach (Note note in obj.Notes.ToList())
            {
                // Note ile ilişkili like ların silinmesi gerekiyor
                foreach (Liked like in note.Likes.ToList())
                {
                    likedMan.Delete(like);
                }

                // Note ile ilişkili commentlerin ların silinmesi gerekiyor

                foreach (Comment comment in note.Comments.ToList())
                {
                    //commentMan.Delete(comment);
                }
                noteMan.Delete(note);
            }

            return base.Delete(obj);
        }
    }
}
