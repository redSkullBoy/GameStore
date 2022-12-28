using GameStore.Data.Entities;

namespace GameStore.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = new();
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
