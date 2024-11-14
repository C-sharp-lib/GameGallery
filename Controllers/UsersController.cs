using GameGallery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameGallery.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
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

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("LoginUser")]
        [ValidateAntiForgeryToken]
        public IActionResult LoginUser(string email, string password)
        {
            var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Login");
            }
            var passwordHasher = new PasswordHasher<Users>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "GameGallery");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View("Login");
            }
        }
        [HttpPost("RegisterUser")]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(RegisterViewModel newuser)
        {
            Users CheckEmail = _context.Users
                .Where(u => u.Email == newuser.Email)
                .SingleOrDefault();

            if (CheckEmail != null)
            {
                ViewBag.errors = "That email already exists";
                return RedirectToAction("Register");
            }
            if (ModelState.IsValid)
            {
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                Users newUser = new Users
                {
                    UserId = newuser.UserId,
                    FirstName = newuser.FirstName,
                    LastName = newuser.LastName,
                    Email = newuser.Email,
                    Username = newuser.Username,
                    Password = Hasher.HashPassword(newuser, newuser.Password)
                };
                _context.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View("Register");
            }
        }

        [HttpGet("Users/Profile/{userId}")]
        public async Task<IActionResult> Profile(int userId)
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
            var reviews = await _context.GameReviews.Where(u => u.UserId == userId)
                .Include(u => u.Reviews).Include(u => u.Games).ToListAsync();
            if (user == null)
            {
                return View("Login");
            }

            ViewBag.user = ActiveUser;
            ViewBag.theUser = user;
            ViewBag.gameReviews = reviews;
            return View();
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }

        [HttpPost("DeleteReviewConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReviewConfirmed(int reviewId)
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewId == reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            ViewBag.user = ActiveUser;
            return RedirectToAction(nameof(Profile), new { id = ActiveUser.UserId });
        }
        [HttpPost("Users/UpdateProfile/{UserId}")]
        public async Task<IActionResult> UpdateProfile(int UserId, string firstName, string lastName, string bio, string imageUrl) 
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            if (ModelState.IsValid)
            {
                Users user = await _context.Users.Where(u => u.UserId == UserId).SingleOrDefaultAsync();
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Bio = bio;
                user.ImageUrl = imageUrl;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Profile), new {Id = UserId});
            }
            return View(nameof(Profile), new { Id = UserId });
        }
    }
}