using System.ComponentModel.DataAnnotations;

namespace Tinderro.API.Dtos
{
    public class UserForRegisterDto
    {

        // Object do transferu
        // Poniewaz przy rejestracji przekazujemy tylko username i password to ta klasa ma wlasnie tylko to
        // Bedzie to przekazywane dalej

        [Required(ErrorMessage="Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Required(ErrorMessage="Haslo jest wymagane")]
        [StringLength(12, MinimumLength=6,  ErrorMessage="Hasło musi składać się od 4 do 12 znaków")]
        public string Password { get; set; }
    }
}