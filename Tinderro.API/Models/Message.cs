using System;

namespace Tinderro.API.Models
{
    public class Message
    {
         public int Id { get; set; }
         // ponizsze 2 linijki wiaza userkow z messedzem
         // jezeli dodamy wiadomosc o senderId = 1 to te linijki POWIAZA wiadomosc z userkiem i bedzie dobrze
        public int SenderId { get; set; }
        public User Sender { get; set; } // tak tylko dodane zeby moc wyswietlic nadawce, potrzebne do RELACJI!
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        public string Content { get; set; } // zawartosc wiadomosci
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; } // jest pytajnik bo data bedzie DOPIERO po odczytaniu wadomosci a nie od razu
        public DateTime DateSent { get; set; }
        public bool SenderDeleted { get; set; } // jak nadawca usunie wiadomosc to zmieni sie flaga
        public bool RecipientDeleted { get; set; } // jak odbiorca usunie wiadomosc
    }
}