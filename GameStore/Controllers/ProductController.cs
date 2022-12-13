using GameStore.Data;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class ProductController : Controller
    {
        private StoreDbContext _context;
        public int PageSize = 4;

        public ProductController(StoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? category, int productPage = 1)
        {

            var result = new ProductsListViewModel
            {
                Products = _context.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null
                        ? _context.Products.Count()
                        : _context.Products.Where(e =>
                            e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(result);
        }
    }
}
