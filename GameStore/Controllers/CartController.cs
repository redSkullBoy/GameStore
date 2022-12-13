using GameStore.Data;
using GameStore.Data.Entities;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class CartController : Controller
    {
        private StoreDbContext _context;
        private Cart cart;

        public CartController(StoreDbContext context, Cart cartService)
        {
            _context = context;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId,
                string returnUrl)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
