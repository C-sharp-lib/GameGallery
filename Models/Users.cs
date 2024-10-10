using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameGallery.Models
{
    public class Users : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Bio { get; set; }

        public string? ImageUrl { get; set; }

        public string Username { get; set; }
        public ICollection<GameReviews> GameReviews { get; set; }
        public ICollection<UserComments> UserComments { get; set; }
        public ICollection<BlogPosts> BlogPosts { get; set; }
        public Users()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
