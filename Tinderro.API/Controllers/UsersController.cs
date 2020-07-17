using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tinderro.API.Data;
using Tinderro.API.Dtos;
using Tinderro.API.Helpers;
using Tinderro.API.Models;

namespace Tinderro.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [ApiController] //jezeli zakomentujemy to to juz nie bedzie apicontroller tylko zwykly MVC controller 
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepo, IMapper mapper) // dodajemy mapera z nugetpacket (taka dluga nazwa cos z injection)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams) // [FromQuerry] powoduje ze dane sa pobierane z url
        {
            // ----------------------------- Do usuniecia zalogowanego uzytkownika z listy i do ustawiania przeciwnej plci do wyszukowania
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromDataBase = await _userRepo.GetUser(userId);

            userParams.UserId = userId;

            // ten filterek jest tu bo trzeba pobrac uzytkownika zeby sie dowiedziec o plci a po co pobierac w 2 miejsach nie?
            if (string.IsNullOrEmpty(userParams.Gender)) // jezeli nie podal co chce wyszukac to wyszukaa sie przeciwnie do jego plci
            {
                userParams.Gender = userFromDataBase.Gender == "mężczyzna" ? "kobieta" : "mężczyzna";
            }
            // -----------------------------
 
            var users = await _userRepo.GetUsers(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            // to jest chyba tylko do wyswietlenia w postmanie w hedersach? jakie ostatecznie byly parametry
            // AddPagination pochodzi z Helpers/Extension
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepo.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            // ten user jest z ControllBase 
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromDataBase = await _userRepo.GetUser(id);
            _mapper.Map(userForUpdateDto, userFromDataBase);
            // metoda SaveAll zwraca true albo false wiec jak zapis powiodl sie to nic nie zwraca
            if (await _userRepo.SaveAll())
                return NoContent();
            
            throw new Exception("Nie masz uprawnień");
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> Like(int id, int recipientId)
        {
             if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var like = await _userRepo.GetLike(id, recipientId);

            if (like != null)
                throw new Exception("Juz lubisz tego uzytkownika");

            if (await _userRepo.GetUser(recipientId) == null)
                return NotFound();

            // jezeli juz nie ma lajka i jest uzytkownik xd to robimy like
            like = new Like
            {
                UserLikesId = id,
                SomeoneLikesMeId = recipientId
            };

            _userRepo.Add<Like>(like);

            if (await _userRepo.SaveAll())
                return Ok();

            return BadRequest("Nie mozna polubic uzytkownika");
            
        }
    }
}