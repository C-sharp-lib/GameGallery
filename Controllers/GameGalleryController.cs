using GameGallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GameGallery.Controllers
{
    public class GameGalleryController : Controller
    {
        private ApplicationDbContext _context;
        public GameGalleryController(ApplicationDbContext context)
        {
            _context = context;
        }

        private Users ActiveUser
        {
            get
            {
                return _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();
            }
        }

        public async Task<IActionResult> Index()
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            var genreList = await _context.Genres
                .Include(g => g.GameGenres).ThenInclude(g => g.Games)
                .ToListAsync();
            ViewBag.user = ActiveUser;
            return View(genreList);
        }
        [HttpGet("GameGallery/GetGames/{genreId}")]
        public async Task<IActionResult> GetGames(int genreId)
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            var gameList = await _context.GameGenres
                .Where(gg => gg.Genres.GenreId == genreId)
                .Include(g => g.Games).ThenInclude(g => g.GameReviews)
                .ToListAsync();
            //ViewBag.games = gameList;
            ViewBag.user = ActiveUser;
            return View(gameList);
        }

        [HttpGet("GameGallery/GetGame/{gameId}")]
        public async Task<IActionResult> GetGame(int gameId)
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            var game = await _context.GameGenres
                .Where(game => game.Games.GameId == gameId)
                .Include(g => g.Games)
                .Include(gg => gg.Genres)
                .FirstOrDefaultAsync();
            if (game == null)
            {
                return NotFound();
            }
            var reviews = await _context.GameReviews.Where(u => u.Games.GameId == gameId)
                .Include(u => u.Reviews).Include(u => u.Games).Include(u => u.Users).ToListAsync();
            int reviewCount = _context.GameReviews.Where(u => u.Games.GameId == gameId)
                .Include(u => u.Reviews).Count();
            int averageRating = 0;
            if (reviews.Any())
            {
                averageRating = (int)reviews.Average(r => r.Reviews.StarRating) / reviewCount;
            }
            if (reviews == null)
            {
                return NotFound();
            }

            ViewBag.user = ActiveUser;
            ViewBag.reviews = reviews;
            ViewBag.theGame = game;
            ViewBag.reviewCount = averageRating;
            return View();
        }
        [HttpPost("GameGallery/AddReview/{gameId}")]
        public async Task<IActionResult> AddReview(int gameId, Reviews reviews)
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            if (ModelState.IsValid)
            {
                Reviews rev = new Reviews
                {
                    ReviewId = reviews.ReviewId,
                    Description = reviews.Description,
                    StarRating = reviews.StarRating
                };
                GameReviews gameReviews = new GameReviews
                {
                    UserId = ActiveUser.UserId,
                    GameId = gameId,
                    ReviewId = rev.ReviewId
                };
                _context.Reviews.Add(rev);
                await _context.SaveChangesAsync();
                gameReviews.ReviewId = rev.ReviewId;
                _context.GameReviews.Add(gameReviews);
                await _context.SaveChangesAsync();
                ViewBag.user = ActiveUser;
                return RedirectToAction("Index", "GameGallery");
            }
            return View("Index");
        }
    }
}
