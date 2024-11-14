using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameGallery.Models
{
    public class BlogPosts : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Description { get; set; }
        public string? ImageOne { get; set; }
        public string? ImageTwo { get; set; }
        public string? ImageThree { get; set; }
        public int? UserId { get; set; }
        public Users User { get; set; }

        public BlogPosts()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
