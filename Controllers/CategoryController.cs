using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Product;

namespace TechStoreMVC.Controllers
{
    public class CategoryController : BaseController
    {
        private DatabaseContext _context;

        public CategoryController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(string categoryName, List<string> brands, string? sort, string? way)
        {
            if (categoryName == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Category? category = _context.Categories.SingleOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Product> products = category.Products;
            ViewBag.Brands = products.Select(p => p.Brand).ToList();

            if (sort != null && sort == "price")
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

            ViewBag.Products = products;

            return View(new ProductToBasketModel(categoryName));
        }
    }
}
