namespace GameGallery.Models
{
    public class UserBlogComments
    {
        public int UserId { get; set; }
        public Users Users { get; set; }
        public int BlogPostId { get; set; }
        public BlogPosts BlogPosts { get; set; }
        public int CommentId { get; set; }
        public Comments Comments { get; set; }
    }
}
