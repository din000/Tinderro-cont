using System;

namespace Tinderro.API.Dtos
{
    public class PhotoFiorDetailedDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }     // Opis
        public DateTime DateAdded { get; set; }     // Data dodania
        public bool IsMain { get; set; }            // Czy zdjęcie jest główne
    }
}