



using Genratax.Data.Abstrack;
using Genratax.Data.Concrete.EfCore;
using Genratax.Entity;

namespace Genratax.Data.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private readonly ForumContext _context;

        public EfUserRepository(ForumContext context){
            _context = context;
        }
        public IQueryable<User> Users => _context.Users;

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}