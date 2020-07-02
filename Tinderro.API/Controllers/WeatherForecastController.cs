using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tinderro.API.Data;
using Tinderro.API.Models;

namespace Tinderro.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DaneController : ControllerBase
    {
        public IUserRepository _Repo { get; set; }

        private readonly DataContext _context;
        public DaneController(DataContext context,
                                IUserRepository repo)
        {
            _Repo = repo;
            _context = context;
        }

        [HttpGet("SEX")]
        public async Task<Photo> GetPhoto()
        {
            // var photo = await _context.photos.FirstOrDefaultAsync(p => p.Id == 4);
            // return photo;
            var photo = await _Repo.GetPhoto(4);
            return photo;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Geta()
        {
            var imiona = await _context.users.ToListAsync();
            return imiona;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User dane)
        {
            _context.users.Add(dane);
            _context.SaveChanges();
            return Ok(dane);
        }

        [HttpPut("(id)")]
        public void Put(int id, [FromBody] User dane)
        {
            var daneZbazy = _context.users.Find(id);
            daneZbazy.Username = dane.Username;
            _context.users.Update(daneZbazy);
            _context.SaveChanges();
        }

        [HttpDelete("(id)")]
        public void Delete(int id)
        {
            var daneZDupy = _context.users.Find(id);
            _context.users.Remove(daneZDupy);
            _context.SaveChanges();
        }
    }
}
