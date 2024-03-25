

using Genratax.Entity;

namespace Genratax.Data.Abstrack
{
    public interface ICommentRepository{
        IQueryable<Comment> Comments {get;}
        void CreateComment(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);
    }
    
}