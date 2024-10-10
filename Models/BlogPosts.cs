using System.ComponentModel.DataAnnotations;

namespace GameGallery.Models
{
    public class BlogPosts : BaseEntity
    {
        [Key]
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImageUrlOne { get; set; }
        public string ImageUrlTwo { get; set; }
        public string ImageUrlThree { get; set; }
        public Users User { get; set; }
        public int UserBlogId { get; set; }

        public BlogPosts()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
