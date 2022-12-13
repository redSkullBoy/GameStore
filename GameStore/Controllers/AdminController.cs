using GameStore.Data;
using GameStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class AdminController : Controller
    {
        private StoreDbContext _context;

        public AdminController(StoreDbContext context)
        {
            _context = context;
        }

        public ViewResult Index() => View(_context.Products);

        public ViewResult Edit(int productId) =>
            View(_context.Products
                .FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                _context.SaveChanges();

                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var deletedProduct = _context.Products.FirstOrDefault(s => s.ProductID == productId);

            if (deletedProduct != null)
            {
                _context.Products.Remove(deletedProduct);
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
