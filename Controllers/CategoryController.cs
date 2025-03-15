using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Attributes;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Category;
using TechStoreMVC.Models.Product;

namespace TechStoreMVC.Controllers
{
    //Async experimenting ahead, but everything should work nicely
    public class CategoryController : BaseController
    {
        private DatabaseContext _context;

        public CategoryController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [RequiresRole("admin")]
        public async Task<IActionResult> Create(CategoryCreateModel ccm)
        {
            if (!ModelState.IsValid) {
                TempData["Message"] = "Invalid data was entered";
                TempData["MessageType"] = "danger";
                return View(ccm);
            }

            Category category = new Category(ccm.Id, ccm.Name);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListCategories", "Administration");
        }

        [RequiresRole("admin")]
        public IActionResult Create()
        {
            return View(new CategoryCreateModel());
        }

        [RequiresRole("admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Category? category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                TempData["Message"] = "There was an error retrieving this category!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("List", "Categories");
            }

            return View(new CategoryCreateModel(category.Id, category.Name));
        }

        [HttpPost]
        [RequiresRole("admin")]
        public async Task<IActionResult> Edit(CategoryCreateModel ccm)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid data was entered";
                TempData["MessageType"] = "danger";
                return View(ccm);
            }

            Category? category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == ccm.Id);

            if (category == null)
            {
                TempData["Message"] = "There was an error retrieving this category!";
                TempData["MessageType"] = "danger";
                return View(ccm);
            }

            category.Name = ccm.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListCategories", "Administration");
        }

        [RequiresRole("admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Category? category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                TempData["Message"] = "There was an error retrieving this category!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("ListCategories", "Administration");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListCategories", "Administration");
        }
    }
}
