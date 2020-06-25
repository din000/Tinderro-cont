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

        private readonly DataContext _context;
        public DaneController(DataContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public async Task<IEnumerable<User>> Geta()
        {
            var imiona =  await _context.users.ToListAsync();
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
