using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameGallery.Models
{
    public class BlogPostViewModel : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogPostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public IFormFile? ImageOne { get; set; }
        [NotMapped]
        public IFormFile? ImageTwo { get; set; }
        [NotMapped]
        public IFormFile? ImageThree { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }
        public Users User { get; set; }

    }
}
