



using Genratax.Data.Abstrack;
using Genratax.Data.Concrete.EfCore;
using Genratax.Entity;

namespace Genratax.Data.Concrete
{
    public class EfPostRepository : IPostRepository
    {
        private readonly ForumContext _context;

        public EfPostRepository(ForumContext context){
            _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public void EditPost(Post post)
        {
            var entity = _context.Posts.FirstOrDefault(i=>i.PostId == post.PostId);
            if(entity !=null){
                entity.Title = post.Title;
                entity.Url = post.Url;
                entity.Content = post.Content;
                entity.Image = post.Image;

                _context.SaveChanges();
            }
        }

    }
}