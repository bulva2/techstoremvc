using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;

namespace TechStoreMVC.Controllers
{
    public class ReviewController : BaseController
    {
        private DatabaseContext _context;

        public ReviewController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostReview(int id, string review, int stars)
        {
            Product? p = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (p == null)
            {
                return NotFound();
            }

            Review rev = new Review(0, p, review, stars);
            await _context.Reviews.AddAsync(rev);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Product", new { id = id });
        }
    }
}
