using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Entities
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите первую строку адреса")]
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название города")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название штата")]
        public string? State { get; set; }

        public string? Zip { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название страны")]
        public string? Country { get; set; }

        public bool GiftWrap { get; set; }

        [BindNever]
        public bool Shipped { get; set; }
    }
}
