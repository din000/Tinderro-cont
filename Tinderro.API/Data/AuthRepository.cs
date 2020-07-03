using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tinderro.API.Models;

namespace Tinderro.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        #region public methods
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;
            
            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;
        
            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHashSalt(password, out passwordHash, out passwordSalt); //referencja bo pozniej tamto bedzie UZYWANE a chcemy jest ZMIENIC

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExist(string username)
        {
            if(await _context.users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        #endregion
        #region private mehods
        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (passwordHash[i] != computedHash[i])
                        return false;
                }
                return true;
            }
        }
        #endregion
    }
}