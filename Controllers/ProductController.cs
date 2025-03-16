using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            ViewBag.Brands = products.Select(p => p.Brand).ToList();

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

            ViewBag.Reviews = p.Reviews;

            return View(new ProductViewModel(p.Id, p.Brand, p.Model, p.Type, p.Price, p.Description, 0));
        }
    }
}
