using System;
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
            var users = _context.users.Include(p => p.Photos).AsQueryable(); // asqueryable musi byc zeby filterki dzialaly
            // -------------- filterki
            users = users.Where(u => u.Id != 3);
            users = users.Where(u => u.Gender == userParams.Gender);

            if (userParams.MinAge != 18 || userParams.MaxAge != 100)
            {
                var minDate = DateTime.Today.AddYears(-userParams.MaxAge -1);
                var maxDate = DateTime.Today.AddYears(-userParams.MinAge);
                users = users.Where(u => u.DateOfBirth >= minDate && u.DateOfBirth <= maxDate);
            }

            if (userParams.ZodiacSign != "wszystkie")
                users = users.Where(u => u.ZodiacSign == userParams.ZodiacSign);
            // --------------

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