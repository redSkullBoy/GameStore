using GameStore.Data;
using GameStore.Data.Entities;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace GameStore.Controllers
{
    public class AdminController : Controller
    {
        private StoreDbContext _context;

        public AdminController(StoreDbContext context)
        {
            _context = context;
        }

        public ViewResult Index(string? search = null)
        {
            var products = _context.Products
                    .OrderBy(p => p.ProductID);

            if (null != search)
            {
                products = (IOrderedQueryable<Data.Entities.Product>)products.Where(s => s.Name.Contains(search));
            }

            var result = new ProductsListViewModel
            {
                Products = products,
                Search = search,
            };

            return View(result);
        }

        public ViewResult Edit(int productId) 
        {
            var product = _context.Products
                .Include(p => p.Image)
                .FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

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

        public IActionResult UploadImg(IFormFile uploadImg, int productId)
        {
            if (uploadImg == null || uploadImg.Length == 0)
            {
                ModelState.AddModelError("Image", "Изображение не выбрано");

                return RedirectToAction("Edit", new { productId = productId });
            }

            using (var memoryStream = new MemoryStream())
            {
                uploadImg.CopyTo(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    var img = new Image()
                    {
                        Content = memoryStream.ToArray()
                    };

                    _context.Images.Add(img);
                    _context.SaveChanges();

                    var product = _context.Products.First(s => s.ProductID == productId);
                    product.ImageID = img.ImageID;
                    _context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Image", "Файл должен быть меньше 2 мб.");
                }
            }

            return RedirectToAction("Edit", new { productId = productId });
        }
    }
}
