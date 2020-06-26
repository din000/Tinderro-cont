using System.Collections.Generic;
using System.Threading.Tasks;
using Tinderro.API.Models;

namespace Tinderro.API.Data
{
    public interface IUserRepository : IGenericRepository
    {
         Task<IEnumerable<User>> Getusers();
         Task<User> GetUser(int id);
    }
}