using System.ComponentModel.DataAnnotations;

namespace AI_Chatbot.DTOs
{
    public class EditMessageDto
    {
        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
