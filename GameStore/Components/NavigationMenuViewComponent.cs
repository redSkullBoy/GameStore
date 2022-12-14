using GameStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private StoreDbContext _context;

        public NavigationMenuViewComponent(StoreDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string currentCategory)
        {
            ViewBag.SelectedCategory = currentCategory;

            var result = _context.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(result);
        }
    }
}
