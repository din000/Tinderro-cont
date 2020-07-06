using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tinderro.API.Helpers;
using Tinderro.API.Models;

namespace Tinderro.API.Data
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        // przed page list
        // public async Task<IEnumerable<User>> Getusers()
        // {
        //     var users = await _context.users.Include(p => p.Photos).ToListAsync();
        //     return users;
        // }

        public async Task<PageList<User>> GetUsers (UserParams userParams)
        {
            var users = _context.users.Include(p => p.Photos);
            return await PageList<User>.CreateListAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photoFromDataBase = await _context.photos.FirstOrDefaultAsync(p => p.Id == id);
            return photoFromDataBase;
        }

        public async Task<Photo> GetMainPhoto(int userId)
        {
            return await _context.photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(i => i.IsMain);
        }



        // public async Task<Photo> TestowePobranieZdj(int id)
        // {
        //     var photoFromDataBase = await _context.photos.FirstOrDefaultAsync(p => p.Id == 4);
        //     return photoFromDataBase;
        // }
    }
}