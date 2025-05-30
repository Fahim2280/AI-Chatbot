namespace AI_Chatbot.DTOs
{
    public class ChatResponseDto
    {
        public int Id { get; set; }  // Bot message ID
        public int UserMessageId { get; set; }  // User message ID - ADD THIS
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}
