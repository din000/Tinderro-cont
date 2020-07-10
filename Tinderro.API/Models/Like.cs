namespace Tinderro.API.Models
{
    public class Like
    {
        public int UserLikesId { get; set; }    // ja kogos/ moje id
        public int SomeoneLikesMeId { get; set; }   // ktos mnie
        public User UserLikes { get; set; }
        public User SomeoneLikes { get; set; }
    }
}