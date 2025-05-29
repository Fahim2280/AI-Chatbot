using System.ComponentModel.DataAnnotations;

namespace AI_Chatbot.DTOs
{
    public class ApproveMessageDto
    {
        [Required]
        public bool IsApproved { get; set; }
    }
}
