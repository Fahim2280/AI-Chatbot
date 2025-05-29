using AI_Chatbot.DTOs;
using AI_Chatbot.Models;

namespace AI_Chatbot.Interfaces
{
    public interface IChatService
    {
        Task<ChatResponseDto> SendMessageAsync(int userId, ChatMessageDto dto);
        Task<List<ChatMessage>> GetChatHistoryAsync(int userId, string sessionId);
        Task<List<ChatRespons>> GetAllChatResponsesAsync();
        Task<ChatMessage?> EditMessageAsync(int userId, int messageId, EditMessageDto dto);
        Task<bool> SoftDeleteMessageAsync(int userId, int messageId);
        Task<ChatMessage?> ApproveMessageAsync(int messageId, bool isApproved);
    }
}
