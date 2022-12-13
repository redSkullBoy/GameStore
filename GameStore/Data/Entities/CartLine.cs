namespace GameStore.Data.Entities
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public int Quantity { get; set; }

        public int ProductID { get; set; }
        public Product? Product { get; set; }

        public int OrderID { get; set; }
        public Order? Order { get; set; }
    }
}
