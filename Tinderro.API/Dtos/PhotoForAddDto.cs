using System;
using Microsoft.AspNetCore.Http;

namespace Tinderro.API.Dtos
{
    public class PhotoForAddDto
    {
        public PhotoForAddDto()
        {
            DateAdded = DateTime.Now;
        }

        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string Public_id { get; set; }
    }
}
