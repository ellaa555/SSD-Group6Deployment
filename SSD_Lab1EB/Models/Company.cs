using System.ComponentModel.DataAnnotations;

namespace SSD_Lab1EB.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Years In Business")]
        [Range(0, 200)]
        public int YearsInBusiness { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string Website { get; set; } = string.Empty;
        public string? Province { get; set; }

    }
}
