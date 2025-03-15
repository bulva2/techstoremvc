using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Category;
using TechStoreMVC.Models.Order;

namespace TechStoreMVC.Controllers
{
    public class AdministrationController : AdminController
    {
        private DatabaseContext _context;

        public AdministrationController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListOrders()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            List<OrderViewModel> ovm = orders.Select(o => new OrderViewModel(o.Id, o.TotalPrice, o.PaymentMethod.Name, o.Address, o.Status, o.OrderItems)).ToList();
            return View(ovm);
        }

        public async Task<IActionResult> ListCategories()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            List<CategoryCreateModel> ccm = categories.Select(c => new CategoryCreateModel(c.Id, c.Name)).ToList();

            return View(ccm);
        }
    }
}
