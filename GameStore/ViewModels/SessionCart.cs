using GameStore.Data.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameStore.ViewModels
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;

            var cart = new SessionCart();

            var sessionData = session?.GetString("Cart");
            if(!string.IsNullOrEmpty(sessionData))
                cart = JsonSerializer.Deserialize<SessionCart>(sessionData);

            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);

            Session?.SetString("Cart", JsonSerializer.Serialize(this));
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);

            Session?.SetString("Cart", JsonSerializer.Serialize(this));
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("Cart");
        }
    }
}
