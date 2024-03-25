

using Genratax.Entity;

namespace Genratax.Data.Abstrack
{
    public interface IPostRepository{
        IQueryable<Post> Posts {get;}
        void CreatePost(Post post);

        void EditPost(Post post);
         void DeletePost(Post post);
        
    }
    
}