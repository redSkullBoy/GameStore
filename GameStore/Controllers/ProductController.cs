using GameStore.Data;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class ProductController : Controller
    {
        private StoreDbContext _context;
        public int PageSize = 6;

        public ProductController(StoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? category, int productPage = 1, string? search = null)
        {
            var products = _context.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID);

            if(null != search)
            {
                products = (IOrderedQueryable<Data.Entities.Product>)products.Where(s => s.Name.Contains(search));
            }

            var productsPage = products
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize);

            var result = new ProductsListViewModel
            {
                Products = productsPage,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = products.Count(),
                },
                CurrentCategory = category,
                Search = search,
            };

            return View(result);
        }

        public IActionResult Details(int id, string returnUrl)
        {
            var model = new ProductViewModel { 
                ReturnUrl= returnUrl,
            };

            var itemDb = _context.Products.FirstOrDefault(s => s.ProductID == id);

            if (itemDb != null)
            {
                model.Product = itemDb;
            }

            return View(model);
        }
    }
}
