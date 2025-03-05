using Microsoft.AspNetCore.Mvc;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Models.Basket;
using TechStoreMVC.Models.Category;

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

        public IActionResult AddToBasket(CategoryViewModel categoryViewModel, int id, string category)
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

            Product? product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            BasketItem basketItem = new BasketItem(0, product, categoryViewModel.Quantity, myBasket);

            _context.BasketItem.Add(basketItem);
            _context.SaveChanges();

            TempData["Message"] = $"{categoryViewModel.Quantity}x {product.Brand} {product.Model} has been added to your basket. ^^";
            TempData["MessageType"] = "success";
            return RedirectToAction("Index", "Category", new { categoryName = category });
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
