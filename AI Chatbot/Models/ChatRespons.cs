using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Chatbot.Models
{
    public class ChatRespons
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

       
        [ForeignKey("User")]
        public int? UserId { get; set; }

        [StringLength(100)]
        public string? SessionId { get; set; }

        [ForeignKey("ChatMessage")]
        public int? ChatMessageId { get; set; }

        
        public virtual Register? User { get; set; }
        public virtual ChatMessage? ChatMessage { get; set; }
    }
}
