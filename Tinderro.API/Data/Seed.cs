using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Tinderro.API.Models;

namespace Tinderro.API.Data
{
    // ta klasa ma na celu tylko zaladowac jakies przykladowe dane wygenerowane wczesniej ze strony generate json

    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()   // tu chyba bylo sciagane z nugeta JwtAngular !!!!!!!!!
        {   
            if (!_context.users.Any())
            {
                var userData = File.ReadAllText("Data/UserSeedData.json"); // zczytujemy wszystko z jsona
                var users = JsonConvert.DeserializeObject<List<User>>(userData); // serializujemy zczytane dane zeby pozniej przypisac
                
                foreach (var item in users)  // item to pojedynczy user, ta penta ma na celu ustawienia hasla ("password") i dodaniu wszystkich uzytkownikow do bazy
                {
                    byte[] passwordSalt, passwordHash;
                    CreatePasswordHashSalt("password", out passwordHash, out passwordSalt); // przekazujemy z gory ustalone haslo na "password"

                    item.PasswordHash = passwordHash;
                    item.PasswordSalt = passwordSalt;
                    item.Username.ToLower();

                    _context.Add(item);
                }
                _context.SaveChanges();
            }     
        }

        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}