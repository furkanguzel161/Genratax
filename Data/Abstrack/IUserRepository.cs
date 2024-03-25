

using Genratax.Entity;

namespace Genratax.Data.Abstrack
{
    public interface IUserRepository{
        IQueryable<User> Users {get;}
        void CreateUser(User user);
        
    }
    
}