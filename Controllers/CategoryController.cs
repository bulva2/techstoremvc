using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Category;

namespace TechStoreMVC.Controllers
{
    public class CategoryController : SecuredController
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

            CategoryViewModel categoryViewModel = new CategoryViewModel(category.Id, category.Name, category.Products);
            return View(categoryViewModel);
        }
    }
}
