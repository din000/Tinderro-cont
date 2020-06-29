namespace Tinderro.API.Dtos
{
    public class UserForUpdateDto
    {
        public string City { get; set; }            // Miasto
        public string Country { get; set; }         // Kraj

        // Zakładka Info
        public string Growth { get; set; }          // Wygląd - wzrost
        public string EyeColor { get; set; }        // Wygląd - kolor oczu
        public string HairColor { get; set; }       // Wygląd - kolor włosów
        public string MartialStatus { get; set; }   // Formalne - stan cywilny
        public string Education { get; set; }       // Formalne - wykształcenie
        public string Profession { get; set; }      // Formalne - zawód
        public string Children { get; set; }        // Dzieci
        public string Languages { get; set; }       // Języki obce


        // Zakłada  O mnie
        public string Motto { get; set; }           // Motto życiowe
        public string Description { get; set; }     // Opis
        public string Personality { get; set; }     // Osobowość
        public string LookingFor { get; set; }      // Szukam


        // Zakłaka Pasje, zaiteresowania
        public string Interests { get; set; }       // Zainteresowania
        public string FreeTime { get; set; }        // Czas wolny
        public string Sport { get; set; }           // Sport
        public string Movies { get; set; }          // Filmyc
        public string Music { get; set; }           // Muzyka

    }
}