using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSD_Lab1EB.Models
{
    /// <summary>
    /// Defines the Product model used to store product information in the database.
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Category { get; set; }

        [Required]
        [Range(0, 100000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Stock Quantity")]
        [Range(0, 10000)]
        public int StockQuantity { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}