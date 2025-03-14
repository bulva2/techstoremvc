using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
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

        public IActionResult ListOrders()
        {
            List<Order> orders = _context.Orders.ToList();
            List<OrderViewModel> ovm = orders.Select(o => new OrderViewModel(o.Id, o.TotalPrice, o.PaymentMethod.Name, o.Address, o.Status, o.OrderItems)).ToList();
            return View(ovm);
        }
    }
}
