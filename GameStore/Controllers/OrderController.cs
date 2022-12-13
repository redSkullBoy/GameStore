using GameStore.Data;
using GameStore.Data.Entities;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class OrderController : Controller
    {
        private StoreDbContext _context;
        private Cart _cart;

        public OrderController(StoreDbContext context, Cart cartService)
        {
            _context = context;
            _cart = cartService;
        }

        public ViewResult Index() =>
            View(_context.Orders.Where(o => !o.Shipped));

        [HttpPost]
        public IActionResult MarkShipped(int orderID)
        {
            var order = _context.Orders
                .FirstOrDefault(o => o.OrderID == orderID);

            if (order != null)
            {
                order.Shipped = true;
                _context.Add(order);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                foreach(var line in _cart.Lines)
                {
                    line.ProductID = line.Product?.ProductID != null 
                        ? line.Product.ProductID 
                        : 0;
                    line.Product = null;
                }

                order.Lines = _cart.Lines.ToArray();

                _context.Add(order);
                _context.SaveChanges();
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
