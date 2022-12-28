using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GameStore.Data.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название продукта")]
        [DisplayName("Название")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        [DisplayName("Описание")]
        public string Description { get; set; } = String.Empty;

        [Required]
        [Range(0.01, double.MaxValue,
            ErrorMessage = "Пожалуйста, укажите положительную цену")]
        [Column(TypeName = "decimal(8, 2)")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите категорию")]
        [DisplayName("Категрия")]
        public string Category { get; set; } = String.Empty;

        public int? ImageID { get; set; }
        public Image? Image { get; set; }
    }
}
