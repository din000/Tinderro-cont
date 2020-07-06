using System.Collections.Generic;
using System.Threading.Tasks;
using Tinderro.API.Helpers;
using Tinderro.API.Models;

namespace Tinderro.API.Data
{
    public interface IUserRepository : IGenericRepository
    {
         // Task<IEnumerable<User>> Getusers(); przed Page list
         Task<PageList<User>> GetUsers (UserParams userParams); // po Page list
         Task<User> GetUser(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhoto(int id);
    }
}