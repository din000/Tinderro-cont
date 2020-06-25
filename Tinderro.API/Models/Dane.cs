using System.ComponentModel.DataAnnotations;

namespace Tinderro.API.Models
{
    public class Dane
    {
        [Key]
        public int Id { get; set; }
        public string imie { get; set; }
        
    }
}