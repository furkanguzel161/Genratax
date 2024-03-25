



using Genratax.Data.Abstrack;
using Genratax.Data.Concrete.EfCore;
using Genratax.Entity;

namespace Genratax.Data.Concrete
{
    public class EfCommentRepository : ICommentRepository
    {
        private readonly ForumContext _context;

        public EfCommentRepository(ForumContext context){
            _context = context;
        }
        public IQueryable<Comment> Comments => _context.Comments;

        public void CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
        public void EditComment(Comment comment)
        {
            var entity = _context.Comments.FirstOrDefault(i=>i.CommentId == comment.CommentId);
            if(entity !=null){
                entity.Text = comment.Text;
                entity.CommentId = comment.CommentId;


                _context.SaveChanges();
            }
        }
        
    }
}