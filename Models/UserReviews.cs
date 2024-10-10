namespace GameGallery.Models
{
    public class UserReviews
    {
        public int UserId { get; set; }
        public Users Users { get; set; }
        public int ReviewId { get; set; }
        public Reviews Reviews { get; set; }
    }
}
