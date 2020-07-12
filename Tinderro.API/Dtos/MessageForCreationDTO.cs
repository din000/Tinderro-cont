using System;

namespace Tinderro.API.Dtos
{
    public class MessageForCreationDTO
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime DateSent { get; set; } // data wyslania wiadomosci
        public string Content { get; set; } // zawartosc

        public MessageForCreationDTO()
        {
            DateSent = DateTime.Now;
        }
    }
}