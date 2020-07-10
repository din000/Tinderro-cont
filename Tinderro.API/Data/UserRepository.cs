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
            var users = _context.users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable(); // asqueryable musi byc zeby filterki dzialaly
            // -------------- filterki
            users = users.Where(u => u.Id != 3);
            users = users.Where(u => u.Gender == userParams.Gender);
            
            // -------------- lubi/ktos lubi
            if (userParams.UserLikes) // lista kogo ja lubie
            {
                var userLikes = await GetUserLikes(userParams.UserId, userParams.UserLikes);
                users = users.Where(u => userLikes.Contains(u.Id));
            }
            if (userParams.SomeoneLikes) // lista kto mnie lubi
            {
                var someOneLikesMe = await GetUserLikes(userParams.UserId, userParams.UserLikes);
                users = users.Where(u => someOneLikesMe.Contains(u.Id));
            }
            // --------------
            if (userParams.MinAge != 18 || userParams.MaxAge != 100)
            {
                var minDate = DateTime.Today.AddYears(-userParams.MaxAge -1);
                var maxDate = DateTime.Today.AddYears(-userParams.MinAge);
                users = users.Where(u => u.DateOfBirth >= minDate && u.DateOfBirth <= maxDate);
            }

            if (userParams.ZodiacSign != "wszystkie")
                users = users.Where(u => u.ZodiacSign == userParams.ZodiacSign);
            // --------------

            // -------------- sortowanie
            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }
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

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u => u.UserLikesId == userId && u.SomeoneLikesMeId == recipientId);
        }


        // Ienumerable od int bo zwracamy id, userLikes sluzy to okresliania ktora lista ma byc wyswietlona
        // zwraca powiazane identyfikatory ir np 1 1 z tabeli Likes w bazie danych
        private async Task<IEnumerable<int>> GetUserLikes(int id, bool userLikes)
        {
            // pobieramy userka o konkretnym id i dolaczamy info o laikach
            var user = await _context.users.Include(x => x.UserLikes).Include(x => x.SomeoneLikes).FirstOrDefaultAsync(i => i.Id == id);

            // w zaleznosci od boola zwracamy liste ze kogos lubimy albo liste w ktorej ktos nas lubi
            if (userLikes)
            {
                return user.UserLikes.Where(u => u.SomeoneLikesMeId == id).Select(i => i.UserLikesId);
            }
            else
            {
                return user.SomeoneLikes.Where(u => u.UserLikesId == id).Select(i => i.SomeoneLikesMeId);
            }
        }



        // public async Task<Photo> TestowePobranieZdj(int id)
        // {
        //     var photoFromDataBase = await _context.photos.FirstOrDefaultAsync(p => p.Id == 4);
        //     return photoFromDataBase;
        // }
    }
}