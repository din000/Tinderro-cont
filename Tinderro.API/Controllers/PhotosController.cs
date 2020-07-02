using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tinderro.API.Data;
using Tinderro.API.Dtos;
using Tinderro.API.Helpers;
using Tinderro.API.Models;

namespace Tinderro.API.Controllers
{
    [Authorize]
    [Route("api/users/{userid}/[controller]")]   // {userid} nazwa ta musi byc taka sama jaka przekazujemy w metodzie HttpGet albo Post albo...
    [ApiController]
    public class PhotosController : ControllerBase
    {
        public IUserRepository _repository { get; set; }
        public IMapper _mapper { get; set; }
        public IOptions<ClaudinarySettings> _claudinaryConfig { get; set; }
        private Cloudinary _cloudinary;
        public PhotosController(IUserRepository repo, IMapper mapper, IOptions<ClaudinarySettings> claudinary)
        {
            _claudinaryConfig = claudinary;
            _mapper = mapper;
            _repository = repo;

            // to jest od biblioteki cloudinary bla bla
            Account account = new Account(
                _claudinaryConfig.Value.CloudName,
                _claudinaryConfig.Value.ApiKey,
                _claudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userid, [FromForm]PhotoForAddDto photoForAddDto) // FromForm mowi skad zdjecie bedzie pochodzic
        {
            // sprawdza id z id z tokena
            if (userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
    
            var userFromDataBase = await _repository.GetUser(userid);  // pobranie uzytkownika z bazy
 
            // UWAGA DZIWNE ponizsza nazwa file MUSI ZGADZAC SIE z nazwa w POSTMANIE z NAZWA ZDJECIA wtf xd
            var file = photoForAddDto.File;// zrobienie pliku z klasy PhotoForAddDto zeby dane moglybyc zczytane 
            var uploadResault = new ImageUploadResult(); // to ma byc na sztywno i tyle, pozniej bedzie wykorzystane to zwrocenia info o zdj

            if (file.Length > 0) // jezeli plik zostal wczytany
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()  // okreslamy parametry
                    {
                        File = new FileDescription(file.Name, stream), // przekazywane jest imie pliku i plik (stream)
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face") // szerokosc wysokosc wypelnienie i ze ma byc scenrtowane zdj na twarzy
                    };

                    uploadResault = _cloudinary.Upload(uploadParams); // uploadujemy z przekazaniem parametrow i w uploadResault beda zwrocone info z chmury
                }
            }

            photoForAddDto.Url = uploadResault.Url.ToString();  // no i dajemy otrzymane id i url do naszej klasy ktora pozniej przeslemy
            photoForAddDto.Public_id = uploadResault.PublicId;

            var photo = _mapper.Map<Photo>(photoForAddDto); // mapujemy na photo z photoforadddto

            if (!userFromDataBase.Photos.Any(p => p.IsMain)) // sprawdza czy JUZ jakies zdjecie jest glowne
                photo.IsMain = true;

            userFromDataBase.Photos.Add(photo);

            if (await _repository.SaveAll())
            {
                var photoForReturn = _mapper.Map<PhotoForReturnDto>(photo); 
                return CreatedAtAction(nameof(GetPhoto), new { id = photo.Id }, photoForReturn);  // 1 argument to sciezka skad bedzie cos pobierane, 2 - przekazujemy id, 3 - zwracamy zdjecie
                // return CreatedAtRoute(nameof(GetPhoto), photoForReturn, new {id = photo.Id});

            }

            return BadRequest("Nie mozna dodac zdj");
        }


        //[HttpGet("{id}", Name = "GetPhoto")]  // musi byc tak bo createdatroute nie dziala :)
        private async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromDatabase = await _repository.GetPhoto(id);
            // return photoFromDatabase; nie da sie wiec trzeba mapowac

            var photoForReturn = _mapper.Map<PhotoForReturnDto>(photoFromDatabase);
            return Ok(photoForReturn);
        }

        // [HttpGet("id")]
        // public async Task<IActionResult> Dupa(int id)
        // {
        //     var photoFromDatabase = await _repository.GetPhoto(id);

        //     var photo = new Photo();
        //     // // return photoFromDatabase; nie da sie wiec trzeba mapowac

        //     // var photoForReturn = _mapper.Map<PhotoForReturnDto>(photoFromDatabase);
        //     // return photoForReturn;
            

        //     return CreatedAtRoute("GetPhoto", new { id = photoFromDatabase.Id }, photo);
        // }
    }
}