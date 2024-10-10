﻿using System.ComponentModel.DataAnnotations;

namespace GameGallery.Models
{
    public class Genres
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? FullName { get; set; }
        public ICollection<GameGenres> GameGenres { get; set; }
    }
}