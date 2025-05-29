using System.ComponentModel.DataAnnotations;

namespace AI_Chatbot.Models
{
    public class Register
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        
        public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public virtual ICollection<ChatRespons> ChatResponses { get; set; } = new List<ChatRespons>();
    }
}
