using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameGallery.Models
{
    public class GameViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }
        [Required(ErrorMessage ="The game title is required.")]
        [MinLength(3, ErrorMessage = "A minimum of 3 is allowed for first title")]
        [MaxLength(100, ErrorMessage = "A maximum of 100 is allowed for title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The game description is required")]
        [MinLength(10, ErrorMessage = "A minimum of 10 is allowed for description")]
        [MaxLength(10000, ErrorMessage = "A maximum of 10000 is allowed for description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The game developers is required.")]
        public string Developers { get; set; }
        [Required(ErrorMessage = "The game producers is required.")]
        public string Producers { get; set; }
        [Required(ErrorMessage = "The game image url is required.")]
        [Display(Name = "Enter a file for the game.")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "The game system is required.")]
        public string System { get; set; }
        [Required(ErrorMessage = "The game price is required.")]
        [Precision(10, 2)]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "The game starting price is required.")]
        [Precision(10, 2)]
        public decimal? PriceBefore { get; set; }
    }
}
