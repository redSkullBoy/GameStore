using GameStore.Data.Entities;

namespace GameStore.ViewModel
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
            = Enumerable.Empty<Product>();

        public PagingInfo PagingInfo { get; set; } = new();
        public string? CurrentCategory { get; set; }
    }
}
