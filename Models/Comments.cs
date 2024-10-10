using System.ComponentModel.DataAnnotations;

namespace GameGallery.Models
{
    public class Comments : BaseEntity
    {
        [Key]
        public int CommentId { get; set; }
        public string Description { get; set; }
        public ICollection<Users> Users { get; set; }

        public Comments()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
