using GameGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameGallery.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
