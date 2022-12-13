namespace GameStore.ViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; } = new Cart();
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
