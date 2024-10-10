using Microsoft.AspNetCore.Mvc;

namespace GameGallery.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
