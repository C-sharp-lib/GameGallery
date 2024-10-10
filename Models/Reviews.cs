using System.ComponentModel.DataAnnotations;

namespace GameGallery.Models
{
    public class Reviews : BaseEntity
    {
        [Key]
        public int ReviewId { get; set; }
        public string Description { get; set; }
        public decimal StarRating { get; set; }
        public ICollection<GameReviews> GameReviews { get; set; }
        public ICollection<UserReviews> UsersReviews { get; set; }
        public Reviews()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
