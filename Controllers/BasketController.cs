using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Basket;
using TechStoreMVC.Models.Product;

namespace TechStoreMVC.Controllers
{
    public class BasketController : SecuredController
    {
        private DatabaseContext _context;

        public BasketController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Account? acc = _context.Accounts.SingleOrDefault(a => a.Username == HttpContext.Session.GetString("username"));

            if (acc == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            Basket? myBasket = _context.Baskets.SingleOrDefault(b => b.AccountId == acc.Id);

            if (myBasket == null) 
            {
                myBasket = new Basket(0, acc);
                _context.Baskets.Add(myBasket);
                _context.SaveChanges();
            }

            return View(new BasketViewModel(myBasket.Id, myBasket.BasketItems));
        }

        public IActionResult Checkout()
        {
            string username = ViewBag.Username;
            Account? acc = _context.Accounts.SingleOrDefault(a => username == a.Username);

            if (acc == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Email = acc.Email;
            ViewBag.PaymentMethods = _context.PaymentMethods.ToList();

            return View(new OrderCreateModel(ViewBag.Email));
        }

        [HttpPost]
        public IActionResult Checkout(OrderCreateModel orderCreateModel)
        {
            ViewBag.PaymentMethods = _context.PaymentMethods.ToList();

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Entered data is invalid!";
                TempData["MessageType"] = "danger";
                return View(orderCreateModel);
            }

            string username = ViewBag.Username;
            Account? acc = _context.Accounts.SingleOrDefault(a => a.Username == username);

            if (acc == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            Basket? basket = _context.Baskets.SingleOrDefault(b => b.AccountId == acc.Id);

            if (basket == null)
            {
                TempData["Message"] = "Error obtaining basket";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index", "Basket");
            }

            PaymentMethod? paymentMethod = _context.PaymentMethods.SingleOrDefault(p => p.Id == orderCreateModel.PaymentMethodId);

            if (paymentMethod == null)
            {
                TempData["Message"] = "Invalid payment method!";
                TempData["MessageType"] = "danger";
                return View(orderCreateModel);
            }

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    decimal total = basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity);

                    Order order = new Order(orderCreateModel, paymentMethod, acc, total);
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    List<OrderItem> items = basket.BasketItems.Select(bi => new OrderItem(0, bi.Product, bi.Quantity, order)).ToList();
                    _context.OrderItems.AddRange(items);
                    _context.BasketItem.RemoveRange(basket.BasketItems);
                    _context.SaveChanges();

                    basket.BasketItems.Clear();
                    _context.Baskets.Update(basket);
                    _context.SaveChanges();

                    transaction.Commit();

                    TempData["Message"] = $"An order with number #{order.Id} has been successfully placed and is being prepared.";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index", "Basket");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    TempData["Message"] = "An error occurred while processing your order. Please try again later.";
                    TempData["MessageType"] = "danger";
                    return RedirectToAction("Index", "Basket");
                }
            }
        }

        [HttpPost]
        public IActionResult AddProductToBasket(ProductToBasketModel ptb)
        {
            string? username = HttpContext.Session.GetString("username");

            Account? acc = _context.Accounts.SingleOrDefault(a => a.Username == username);

            if (acc == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            Basket? myBasket = _context.Baskets.SingleOrDefault(b => b.AccountId == acc.Id);

            if (myBasket == null)
            {
                myBasket = new Basket(0, acc);
                _context.Baskets.Add(myBasket);
                _context.SaveChanges();
            }

            Product? product = _context.Products.SingleOrDefault(p => p.Id == ptb.Id);

            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            BasketItem basketItem = new BasketItem(0, product, ptb.Quantity, myBasket);

            _context.BasketItem.Add(basketItem);
            _context.SaveChanges();

            TempData["Message"] = $"{ptb.Quantity}x {product.Brand} {product.Model} has been added to your basket. ^^";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Remove(int id)
        {
            BasketItem? item = _context.BasketItem.SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                TempData["Message"] = "We couldn't find the specified item. Try again later.";
                TempData["MessageType"] = "Danger";
                return RedirectToAction("Index", "Basket");
            }

            Basket basket = item.Basket;

            string username = ViewBag.Username;
            Account? myAcc = _context.Accounts.SingleOrDefault(a => a.Username == username);

            if (myAcc == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (basket.AccountId != myAcc.Id)
            {
                TempData["Message"] = "We couldn't find the specified item. Try again later. (ERR: Unauthorized)";
                TempData["MessageType"] = "Danger";
                return RedirectToAction("Index", "Basket");
            }

            _context.BasketItem.Remove(item);
            _context.SaveChanges();

            TempData["Message"] = "The product has been removed from your basket.";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "Basket");
        }
    }
}
