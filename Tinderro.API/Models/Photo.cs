using System;

namespace Tinderro.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }     // Opis
        public DateTime DateAdded { get; set; }     // Data dodania
        public bool IsMain { get; set; }            // Czy zdjęcie jest główne
        
        // te 2 linijki powiazuja zdjecia z konkretnym uzytkownikiem
        // pozwoli to na usuwanie zdj jezeli usuniemy uzytkownika
        public User User { get; set; }
        public int UserId { get; set; }
    
    }
}