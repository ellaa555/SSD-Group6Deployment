using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSD_Lab1EB.Models
{
    /// <summary>
    /// Defines the Service model used to store service information in the database.
    /// </summary>
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Category { get; set; }

        [Required]
        [Display(Name = "Hourly Rate")]
        [Range(0, 100000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyRate { get; set; }

        [Required]
        [Display(Name = "Duration Minutes")]
        [Range(1, 1440)]
        public int DurationMinutes { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }
    }
}