using System.ComponentModel.DataAnnotations;

namespace SSD_Lab1EB.Models
{
    /// <summary>
    /// A message posted to the message board by an authenticated user.
    /// </summary>
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Message")]
        public string Content { get; set; } = string.Empty;

        // Set on the server from the logged in user, never from the form.
        [StringLength(256)]
        [Display(Name = "Posted By")]
        public string? Author { get; set; }

        // Set on the server at the time of posting.
        [Display(Name = "Posted At (UTC)")]
        public DateTime PostedAt { get; set; }
    }
}
