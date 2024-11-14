using GameGallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameGallery.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) 
        { 
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private Users ActiveUser
        {
            get
            {
                return _context.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();
            }
        }

        [HttpGet("Blog")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Posts = await _context.BlogPosts
                .Include(u => u.User)
                .ToListAsync();
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet("Blog/AddBlogPost")]
        public IActionResult AddBlogPost()
        {
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpPost("Blog/UploadBlogPost")]
        public async Task<IActionResult> UploadBlogPost(BlogPostViewModel model) 
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            if (ModelState.IsValid)
            {
                string uniqueFileName1 = null;
                string uniqueFileName2 = null;
                string uniqueFileName3 = null;

                if (model.ImageOne != null && model.ImageOne.Length > 0)
                {
                    var permittedExtensions1 = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension1 = Path.GetExtension(model.ImageOne.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension1) || !permittedExtensions1.Contains(extension1))
                    {
                        ModelState.AddModelError("ImageOne", "Invalid file type. Only images are allowed.");
                        return View(model);
                    }
                    string fileName1 = Path.GetFileNameWithoutExtension(model.ImageOne.FileName);
                    uniqueFileName1 = $"{fileName1}_{Guid.NewGuid()}{extension1}";
                    string uploadsFolder1 = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                    if (!Directory.Exists(uploadsFolder1))
                    {
                        Directory.CreateDirectory(uploadsFolder1);
                    }
                    string filePath = Path.Combine(uploadsFolder1, uniqueFileName1);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageOne.CopyToAsync(fileStream);
                    }
                }

                if (model.ImageTwo != null && model.ImageTwo.Length > 0)
                {
                    // Validate the file (optional but recommended)
                    var permittedExtensions2 = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension2 = Path.GetExtension(model.ImageTwo.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension2) || !permittedExtensions2.Contains(extension2))
                    {
                        ModelState.AddModelError("ImageOne", "Invalid file type. Only images are allowed.");
                        return View(model);
                    }

                    // Create a unique filename to prevent overwriting
                    string fileName2 = Path.GetFileNameWithoutExtension(model.ImageTwo.FileName);
                    uniqueFileName2 = $"{fileName2}_{Guid.NewGuid()}{extension2}";

                    // Combine the path with the Uploads folder
                    string uploadsFolder2 = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");

                    // Ensure the Uploads folder exists
                    if (!Directory.Exists(uploadsFolder2))
                    {
                        Directory.CreateDirectory(uploadsFolder2);
                    }

                    // Full path to save the file
                    string filePath2 = Path.Combine(uploadsFolder2, uniqueFileName2);

                    // Save the file to the server
                    using (var fileStream2 = new FileStream(filePath2, FileMode.Create))
                    {
                        await model.ImageTwo.CopyToAsync(fileStream2);
                    }
                }
                if (model.ImageThree != null && model.ImageThree.Length > 0)
                {
                    // Validate the file (optional but recommended)
                    var permittedExtensions3 = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension3 = Path.GetExtension(model.ImageThree.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension3) || !permittedExtensions3.Contains(extension3))
                    {
                        ModelState.AddModelError("ImageOne", "Invalid file type. Only images are allowed.");
                        return View(model);
                    }

                    // Create a unique filename to prevent overwriting
                    string fileName3 = Path.GetFileNameWithoutExtension(model.ImageThree.FileName);
                    uniqueFileName3 = $"{fileName3}_{Guid.NewGuid()}{extension3}";

                    // Combine the path with the Uploads folder
                    string uploadsFolder3 = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");

                    // Ensure the Uploads folder exists
                    if (!Directory.Exists(uploadsFolder3))
                    {
                        Directory.CreateDirectory(uploadsFolder3);
                    }

                    // Full path to save the file
                    string filePath = Path.Combine(uploadsFolder3, uniqueFileName3);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageThree.CopyToAsync(fileStream);
                    }
                }
                BlogPosts blogPost = new BlogPosts
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageOne = uniqueFileName1 != null ? Path.Combine("Uploads", uniqueFileName1) : null,
                    ImageTwo = uniqueFileName2 != null ? Path.Combine("Uploads", uniqueFileName2) : null,
                    ImageThree = uniqueFileName3 != null ? Path.Combine("Uploads", uniqueFileName3) : null,
                    Subtitle = model.Subtitle,
                    UserId = ActiveUser.UserId
                };
                _context.BlogPosts.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Blog");
            }
            return View(model);
        }


        [HttpGet("Blog/GetBlogItem/{blogPostId}")]
        public async Task<IActionResult> GetBlogItem(int blogPostId) 
        { 
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            var blogItem = await _context.BlogPosts.Where(b => b.BlogPostId == blogPostId).SingleOrDefaultAsync();
            ViewBag.user = ActiveUser;
            return View(blogItem);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePostConfirmed(int postId)
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            var post = await _context.BlogPosts.FindAsync(postId);
            if (post != null)
            {
                _context.BlogPosts.Remove(post);
            }
            await _context.SaveChangesAsync();
            ViewBag.user = ActiveUser;
            return RedirectToAction(nameof(Index), "Blog");
        }

        [HttpGet("Blog/EditBlogItem/{blogPostId}")]
        public async Task<IActionResult> EditBlogItem(
            int blogPostId, string description, string title, string subtitle,
            IFormFile imageOne, IFormFile imageTwo, IFormFile imageThree)
        {
            if (ActiveUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                var blogItem = await _context.BlogPosts.Where(b => b.BlogPostId == blogPostId).SingleOrDefaultAsync();
                blogItem.Title = title;
                blogItem.Subtitle = subtitle;
                blogItem.Description = description;

                if (blogItem.UserId != ActiveUser.UserId)
                {
                    return RedirectToAction("Login", "Users");
                }
                if (imageOne != null && imageOne.Length > 0)
                {
                    string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageOne.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName1);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageOne.CopyToAsync(fileStream);
                    }
                    blogItem.ImageOne = uniqueFileName1;
                }
                if (imageTwo != null && imageTwo.Length > 0)
                {
                    string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageTwo.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName2);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageTwo.CopyToAsync(fileStream);
                    }
                    blogItem.ImageTwo = uniqueFileName2;
                }
                if (imageThree != null && imageThree.Length > 0)
                {
                    string uniqueFileName3 = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageThree.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName3);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageThree.CopyToAsync(fileStream);
                    }
                    blogItem.ImageThree = uniqueFileName3;
                }
                blogItem.UserId = ActiveUser.UserId;
                ViewBag.user = ActiveUser;
                return RedirectToAction(nameof(Index), "Blog");
            }
                return RedirectToAction(nameof(Index), "Blog");
        }
    }
}
