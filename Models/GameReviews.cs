namespace GameGallery.Models
{
    public class GameReviews
    {
        public int GameId { get; set; }
        public Games Games { get; set; }
        public int ReviewId { get; set; }
        public Reviews Reviews { get; set; }
    }
}
