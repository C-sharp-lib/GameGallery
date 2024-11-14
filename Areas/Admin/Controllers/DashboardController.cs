using GameGallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameGallery.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context) 
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
            var games = await _context.Games
                .Include(g => g.GameReviews)
                .ThenInclude(g => g.Reviews)
                .Include(gr => gr.GameGenres)
                .ThenInclude(gr => gr.Genres)
                .ToListAsync();
            ViewBag.Games = games;
            ViewBag.user = ActiveUser;
            return View();
        }
    }
}
