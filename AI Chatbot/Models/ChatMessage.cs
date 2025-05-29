using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Chatbot.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string SessionId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(10)]
        public string Sender { get; set; } = string.Empty; // "User" or "Bot"

        [Required]
        public string Message { get; set; } = string.Empty;

        public bool IsApproved { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

       
        public virtual Register User { get; set; } = null!;
        public virtual ICollection<ChatRespons> ChatResponses { get; set; } = new List<ChatRespons>();
    }
}
