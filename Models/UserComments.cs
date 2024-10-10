namespace GameGallery.Models
{
    public class UserComments
    {
        public int UserId { get; set; }
        public Users Users { get; set; }
        public int CommentId { get; set; }
        public Comments Comments { get; set; }
    }
}
