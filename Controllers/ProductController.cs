using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            ViewBag.ProductViewModels = products.Select(p => new ProductViewModel(p.Id, p.Brand, p.Model, p.Type, p.Price, p.Description, 1)).ToList();
            return View(new ProductToBasketModel());
        }

        public IActionResult Details(int id)
        {
            Product? p = _context.Products.SingleOrDefault(p => p.Id == id);

            if (p == null)
            {
                TempData["Message"] = "There was an error retrieving details about this product.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("", "Home");
            }

            return View(new ProductViewModel(p.Id, p.Brand, p.Model, p.Type, p.Price, p.Description, 0));
        }
    }
}
