using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Order;

namespace TechStoreMVC.Controllers
{
    public class OrderController : SecuredController
    {
        private DatabaseContext _context;

        public OrderController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult List()
        {
            string username = ViewBag.Username;
            Account? acc = _context.Accounts.SingleOrDefault(a => a.Username == username);

            if (acc == null)
            {
                TempData["Message"] = "There was an error retrieving your account data.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Login", "Auth");
            }

            List<OrderViewModel> ovm = new List<OrderViewModel>();

            foreach (Order o in acc.Orders)
            {
                OrderViewModel orderViewModel = new OrderViewModel(o.Id, o.TotalPrice, o.PaymentMethod.Name, o.Address, o.Status, o.OrderItems);
                ovm.Add(orderViewModel);
            }

            return View(ovm);
        }

        public IActionResult Details(int orderId)
        {
            Order? order = _context.Orders.SingleOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                TempData["Message"] = "There was an error retrieving details about this order.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("List", "Order");
            }

            OrderViewModel orderViewModel = new OrderViewModel(order.Id, order.TotalPrice, order.PaymentMethod.Name, order.Address, order.Status, order.OrderItems);
            return View(orderViewModel);
        }
    }
}
