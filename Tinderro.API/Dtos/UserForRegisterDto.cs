using System;
using System.ComponentModel.DataAnnotations;

namespace Tinderro.API.Dtos
{
    public class UserForRegisterDto
    {

        // Object do transferu
        // Poniewaz przy rejestracji przekazujemy tylko username i password to ta klasa ma wlasnie tylko to
        // Bedzie to przekazywane dalej

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }

        [Required(ErrorMessage="Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Required(ErrorMessage="Haslo jest wymagane")]
        [StringLength(12, MinimumLength=6,  ErrorMessage="Hasło musi składać się od 4 do 12 znaków")]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string ZodiacSign { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; } 
        public DateTime LastActive { get; set; }
    }
}