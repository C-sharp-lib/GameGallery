using System.ComponentModel.DataAnnotations;

namespace GameGallery.Models
{
    public class Games
    {
        [Key]
        public int GameId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Developers { get; set; }

        public string Producers { get; set; }

        public string ImageUrl { get; set; }

        public string System { get; set; }

        public decimal? Price { get; set; }
        public decimal? PriceBefore { get; set; }
        public ICollection<GameReviews> GameReviews { get; set; }
        public ICollection<GameGenres> GameGenres { get; set; }
    }
}
