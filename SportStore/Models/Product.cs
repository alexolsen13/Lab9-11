using System.ComponentModel.DataAnnotations.Schema;

namespace SportStore.Models
{
    // Модель продукта
    public class Product
    {
        // Идентификатор продукта
        public long ProductID { get; set; }

        // Название продукта
        public string Name { get; set; }

        // Описание продукта
        public string Description { get; set; }

        // Цена продукта
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        // Категория продукта
        public string Category { get; set; }
    }
}

