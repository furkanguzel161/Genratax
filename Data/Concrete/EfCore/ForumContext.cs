using Genratax.Entity;
using Genratax.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace Genratax.Data.Concrete.EfCore
{
    public class ForumContext:DbContext
    {
    public ForumContext(DbContextOptions<ForumContext> optionsForum):base(optionsForum){

        }
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<User> Users => Set<User>();
         public DbSet<Comment> Comments => Set<Comment>();

    }
}