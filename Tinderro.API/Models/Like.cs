namespace Tinderro.API.Models
{
    public class Like
    {
        public int UserLikesId { get; set; }    // ja kogos/ moje id
        public int SomeoneLikesMeId { get; set; }   // ktos mnie
    
        // te 2 linijki sa chyba po to zeby pozniej moc wywietlic jakichkolwiek userow, tak - sa po to zeby wyswietlic info o nich
        public User UserLikes { get; set; } // ja
        public User SomeoneLikes { get; set; } // user ktorego lubie
    }
}