using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Category;
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

        public IActionResult Index(string categoryName)
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
            ViewBag.Products = products;

            return View(new ProductToBasketModel(categoryName));
        }
    }
}
