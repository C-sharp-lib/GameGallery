using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameGallery.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemId { get; set; }
        public int GameId { get; set; }
        public Games Games { get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; }
        [Precision(10,2)]
        public decimal Price { get; set; }

    }
}
