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
    [Route("api/users/{userId}/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public MessagesController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id) // id to id wiadomosci
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromDataBase = await _repository.GetMessage(id);

            if (messageFromDataBase == null)
                return NotFound();

            return Ok(messageFromDataBase);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery]MessagesParams messagesParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messagesParams.UserId = userId;
            var messagesFromDataBase = await _repository.GetAllMessagesForUser(messagesParams);
            var messageToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromDataBase);

            Response.AddPagination(messagesFromDataBase.CurrentPage, messagesFromDataBase.PageSize, 
                                   messagesFromDataBase.TotalCount, messagesFromDataBase.TotalPages);

            return Ok(messageToReturn);
        }

        [HttpGet("thread/{recipientId}")] // tutaj dajemy recipient id BO w glownym url mamy juz userId !!!
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var messagesFromDataBase = await _repository.GetMessagesThread(userId, recipientId);
            var messagesToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromDataBase);

            return Ok(messagesToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDTO messageForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageForCreationDto.SenderId = userId; // ustawiamy id nadawcy

            var recipient = _repository.GetUser(messageForCreationDto.RecipientId); // odbiorca

            if (recipient == null)
                return BadRequest("Nie mozna znalezc odbiorcy");
            
            var message = _mapper.Map<Message>(messageForCreationDto);

            _repository.Add(message); // mozna dodac do bazy poniewaz jest juz zmapowane na cos co baza dokladnie zna czyli message

            var messageToreturn = _mapper.Map<MessageForCreationDTO>(message);

            if (await _repository.SaveAll())
                return CreatedAtRoute("GetMessage", new { userId, id = message.Id }, messageToreturn);
            
            throw new Exception("Utworzenie wiadomosci nie powiodlo sie przy zapisie");
        }
    }
}