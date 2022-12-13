using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название продукта")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { get; set; } = String.Empty;

        [Required]
        [Range(0.01, double.MaxValue,
            ErrorMessage = "Пожалуйста, укажите положительную цену")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите категорию")]
        public string Category { get; set; } = String.Empty;
    }
}
