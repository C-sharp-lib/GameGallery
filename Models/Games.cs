﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameGallery.Models
{
    public class Games
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Developers { get; set; }

        public string Producers { get; set; }

        public string ImageUrl { get; set; }

        public string System { get; set; }
        [Precision(10, 2)]
        public decimal? Price { get; set; }
        [Precision(10, 2)]
        public decimal? PriceBefore { get; set; }
        public ICollection<GameReviews> GameReviews { get; set; }
        public ICollection<GameGenres> GameGenres { get; set; }
    }
}
