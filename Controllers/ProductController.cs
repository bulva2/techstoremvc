using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Attributes;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Product;

namespace TechStoreMVC.Controllers
{
    public class ProductController : BaseController
    {
        private IWebHostEnvironment _env;
        private DatabaseContext _context;

        public ProductController(DatabaseContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(string? search, string? categoryName, List<string> brands, string? way)
        {
            List<Product> products = _context.Products.ToList();

            if (categoryName != null)
            {
                Category? category = _context.Categories.SingleOrDefault(c => c.Name == categoryName);

                if (category != null)
                {
                    products = category.Products;
                } else
                {
                    products = _context.Products.ToList();
                }
            }

            ViewBag.Brands = products.Select(p => p.Brand).Distinct().ToList();

            if (way != null)
            {
                if (way == "desc")
                    products = products.OrderByDescending(p => p.Price).ToList();
                else if (way == "asc")
                    products = products.OrderBy(p => p.Price).ToList();
            }

            if (brands.Count > 0)
            {
                products = products.Where(p => p.Brand != null && brands.Contains(p.Brand)).ToList();
            }

            if (search != null)
            {
                products = products.Where(p => p.FullText.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.ProductViewModels = products.Select(p => new ProductViewModel(p.Id, p.Brand, p.Model, p.Type, p.Price, p.Description, 1)).ToList();


            if (categoryName != null)
                return View(new ProductToBasketModel(categoryName));
            else
                return View(new ProductToBasketModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            Product? p = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (p == null)
            {
                TempData["Message"] = "There was an error retrieving details about this product.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("", "Home");
            }

            List<Review> reviews = p.Reviews;
            ViewBag.Reviews = p.Reviews;

            decimal rating = 0;

            if (reviews.Count > 0)
            {
                reviews.ForEach(r => rating += r.Rating);
                ViewBag.Rating = Math.Round(rating / reviews.Count, 2);
            } else
            {
                ViewBag.Rating = 0;
            }

            string filename = p.Id + "large.png";
            string path = Path.Combine(_env.WebRootPath, "img", "products");

            ViewBag.LargeImgExists = System.IO.File.Exists(Path.Combine(path, filename)) ? true : false;
            return View(new ProductViewModel(p.Id, p.Brand, p.Model, p.Type, p.Price, p.Description, 0));
        }

        [RequiresRole("admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Product? p = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (p == null)
            {
                TempData["Message"] = "There was an error obtaining the requested product!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index", "Product");
            }

            _context.Products.Remove(p);
            await _context.SaveChangesAsync();

            string filePath = Path.Combine(_env.WebRootPath, "img", "products", $"{p.Id}.png");
            string filePathLarge = Path.Combine(_env.WebRootPath, "img", "products", $"{p.Id}large.png");

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            if (System.IO.File.Exists(filePathLarge))
                System.IO.File.Delete(filePathLarge);

            return RedirectToAction("Index", "Product");
        }

        [RequiresRole("admin")]
        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(new ProductCreateModel());
        }

        [RequiresRole("admin")]
        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateModel pcm)
        {
            if (pcm.Image != null)
            {
                if (pcm.Image.Length > 1024 * 1024 * 10)
                {
                    ModelState.AddModelError("Image", "The uploaded file is too big!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(pcm);
                }

                if (Path.GetExtension(pcm.Image.FileName.ToLower()) != ".png")
                {
                    ModelState.AddModelError("Image", "The uploaded file is not a PNG file!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(pcm);
                }
            }

            if (pcm.LargeImage != null)
            {
                if (pcm.LargeImage.Length > 1024 * 1024 * 10)
                {
                    ModelState.AddModelError("LargeImage", "The uploaded file is too big!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(pcm);
                }

                if (Path.GetExtension(pcm.LargeImage.FileName.ToLower()) != ".png")
                {
                    ModelState.AddModelError("LargeImage", "The uploaded file is not a PNG file!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(pcm);
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(pcm);
            }

            Category? category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == pcm.CategoryId);
            if (category == null)
            {
                TempData["Message"] = "There was an error obtaining the requested category!";
                TempData["MessageType"] = "danger";
                ViewBag.Categories = await _context.Categories.ToListAsync();

                return RedirectToAction("Index", "Product");
            }

            Product p = new Product(0, pcm.Brand, pcm.Model, pcm.Type, pcm.Price, pcm.Description, category);
            await _context.Products.AddAsync(p);
            await _context.SaveChangesAsync();

            if (pcm.Image != null)
            {
                string filename = p.Id + Path.GetExtension(pcm.Image.FileName);
                string path = Path.Combine(_env.WebRootPath, "img", "products");

                using (FileStream fs = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await pcm.Image.CopyToAsync(fs);
                }
            }

            if (pcm.LargeImage != null)
            {
                string filename = p.Id + "large" + Path.GetExtension(pcm.LargeImage.FileName);
                string path = Path.Combine(_env.WebRootPath, "img", "products");

                using (FileStream fs = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await pcm.LargeImage.CopyToAsync(fs);
                }
            }

            TempData["Message"] = "Product added successfully!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "Product");
        }

        [RequiresRole("admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Product? p = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (p == null)
            {
                TempData["Message"] = "There was an error obtaining the requested product!";
                TempData["MessageType"] = "danger";

                return RedirectToAction("Index", "Product");
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(new ProductCreateModel(p.Id, p.CategoryId, p.Brand, p.Model, p.Type, p.Price, p.Description, null, null));
        }

        [RequiresRole("admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateModel pcm)
        {
            Product? p = await _context.Products.SingleOrDefaultAsync(p => p.Id == pcm.Id);
            if (p == null)
            {
                TempData["Message"] = "There was an error obtaining the requested product!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index", "Product");
            }

            if (pcm.Image != null)
            {
                if (pcm.Image.Length > 1024 * 1024 * 10)
                {
                    ModelState.AddModelError("Image", "The uploaded file is too big!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(pcm);
                }
                if (Path.GetExtension(pcm.Image.FileName.ToLower()) != ".png")
                {
                    ModelState.AddModelError("Image", "The uploaded file is not a PNG file!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(pcm);
                }
            }

            if (pcm.LargeImage != null)
            {
                if (pcm.LargeImage.Length > 1024 * 1024 * 10)
                {
                    ModelState.AddModelError("LargeImage", "The uploaded file is too big!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(pcm);
                }

                if (Path.GetExtension(pcm.LargeImage.FileName.ToLower()) != ".png")
                {
                    ModelState.AddModelError("LargeImage", "The uploaded file is not a PNG file!");
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(pcm);
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(pcm);
            }

            Category? category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == pcm.CategoryId);
            if (category == null)
            {
                TempData["Message"] = "There was an error obtaining the requested category!";
                TempData["MessageType"] = "danger";
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(pcm);
            }

            p.Brand = pcm.Brand;
            p.Model = pcm.Model;
            p.Type = pcm.Type;
            p.Price = pcm.Price;
            p.Description = pcm.Description;
            p.Category = category;
            await _context.SaveChangesAsync();

            if (pcm.Image != null)
            {
                string filename = p.Id + Path.GetExtension(pcm.Image.FileName);
                string path = Path.Combine(_env.WebRootPath, "img", "products");
                string fullpath = Path.Combine(path, filename);

                using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                {
                    await pcm.Image.CopyToAsync(fs);
                }
            }

            if (pcm.LargeImage != null)
            {
                string filename = p.Id + "large" + Path.GetExtension(pcm.LargeImage.FileName);
                string path = Path.Combine(_env.WebRootPath, "img", "products");
                string fullpath = Path.Combine(path, filename);

                using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                {
                    await pcm.LargeImage.CopyToAsync(fs);
                }
            }

            TempData["Message"] = "Product edited successfully!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "Product");
        }
    }
}
