using GameGallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GameGallery.Controllers
{
    public class GameGalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GameGalleryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var genreList = await _context.Genres
                .Include(g => g.GameGenres).ThenInclude(g => g.Games)
                .ToListAsync();
            return View(genreList);
        }
        [HttpGet("GameGallery/GetGames/{genreId}")]
        public async Task<IActionResult> GetGames(int genreId)
        {
            var gameList = await _context.GameGenres
                .Where(gg => gg.Genres.GenreId == genreId)
                .Include(g => g.Games).ThenInclude(g => g.GameReviews)
                .ToListAsync();
            //ViewBag.games = gameList;
            return View(gameList);
        }

        [HttpGet("GameGallery/GetGame/{gameId}")]
        public async Task<IActionResult> GetGame(int gameId)
        {
            var game = await _context.GameGenres
                .Where(game => game.Games.GameId == gameId)
                .Include(g => g.Games).ThenInclude(g => g.GameReviews)
                .Include(gg => gg.Genres)
                .FirstOrDefaultAsync();
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

    }
}
