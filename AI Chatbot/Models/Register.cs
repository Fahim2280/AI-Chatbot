using System.ComponentModel.DataAnnotations;

namespace AI_Chatbot.Models
{
    public class Register
    {
        [Key]
        public required int ID { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
