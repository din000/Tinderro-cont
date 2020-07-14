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
        // zwraca powiazane identyfikatory ir np 1 1 z tabeli Likes w bazie danych CHYBA JEDNAK NIE XD albo tak xd
        private async Task<IEnumerable<int>> GetUserLikes(int id, bool userLikes)
        {
            // pobieramy userka o konkretnym id i dolaczamy info o laikach
            var user = await _context.users.Include(x => x.UserLikes).Include(x => x.SomeoneLikes).FirstOrDefaultAsync(i => i.Id == id);

            // w zaleznosci od boola zwracamy liste ze kogos lubimy albo liste w ktorej ktos nas lubi
            if (userLikes)
            {
                // where - no to waruneczek
                // select to co konkretnie wybieramy czyli nie userki a Id
                // userLikes to chyba cala klasa Like z danymi

                // UserLikes JUZ MA TA LISTE !!!!!!!!!! TRZEBA TYLKO DOBRZE SELECTA DAC ZE CHCE SIE ID TEGO KOGOS A NIE SWOJE
                return user.UserLikes.Select(i => i.SomeoneLikesMeId);
                // return user.UserLikes.Where(u => u.SomeoneLikesMeId == id).Select(i => i.UserLikesId);
            }
            else
            {
                return user.SomeoneLikes.Select(i => i.UserLikesId);
                // return user.SomeoneLikes.Where(u => u.UserLikesId == id).Select(i => i.SomeoneLikesMeId);
            }
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PageList<Message>> GetAllMessagesForUser(MessagesParams messagesParams)
        {
            var messages = _context.Messages.Include(u => u.Sender).ThenInclude(p => p.Photos)
                                                  .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                                                  .AsQueryable();

            // switch zwraca albo wiadomosci w srzynce nadawczej, alno przychodzace albo wszystkie
            switch (messagesParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messagesParams.UserId);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messagesParams.UserId);
                    break;
                default: // default czyli NIEPRZECZYTANE wiadomosci
                    messages = messages.Where(u => u.RecipientId == messagesParams.UserId && u.IsRead == false);
                    break;
            }

            // sortowanie po dacie
            messages.OrderByDescending(d => d.DateSent);

            return await PageList<Message>.CreateListAsync(messages, messagesParams.PageNumber, messagesParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessagesThread(int userId, int recipientId)
        {
            var messages = await _context.Messages.Include(u => u.Sender).ThenInclude(p => p.Photos)
                                                  .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                                                  .Where(m => m.RecipientId == userId && m.SenderId == recipientId && m.SenderDeleted == false
                                                  || m.RecipientId == recipientId && m.SenderId == userId && m.SenderDeleted == false)
                                                  .OrderByDescending(m => m.DateSent)
                                                  .ToListAsync();
             
            return messages;
        }



        // public async Task<Photo> TestowePobranieZdj(int id)
        // {
        //     var photoFromDataBase = await _context.photos.FirstOrDefaultAsync(p => p.Id == 4);
        //     return photoFromDataBase;
        // }
    }
}