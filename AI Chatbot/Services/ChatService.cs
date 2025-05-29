using AI_Chatbot.DTOs;
using AI_Chatbot.Interfaces;
using AI_Chatbot.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AI_Chatbot.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public ChatService(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["TavilySettings:ApiKey"];
        }

        public async Task<ChatResponseDto> SendMessageAsync(int userId, ChatMessageDto dto)
        {
            // Save user message
            var userMessage = new ChatMessage
            {
                UserId = userId,
                SessionId = dto.SessionId,
                Sender = "User",
                Message = dto.Message,
                Timestamp = DateTime.UtcNow
            };
            _context.ChatMessages.Add(userMessage);
            await _context.SaveChangesAsync();

            // Call Tavily AI
            var botReply = await CallTavilyAI(dto.Message);

            var botMessage = new ChatMessage
            {
                UserId = userId,
                SessionId = dto.SessionId,
                Sender = "Bot",
                Message = botReply,
                Timestamp = DateTime.UtcNow
            };
            _context.ChatMessages.Add(botMessage);

            
            var chatResponse = new ChatRespons
            {
                Message = botReply,
                Timestamp = DateTime.UtcNow
            };
            _context.ChatResponses.Add(chatResponse);

            await _context.SaveChangesAsync();

            return new ChatResponseDto
            {
                Message = botReply,
                Timestamp = chatResponse.Timestamp
            };
        }

        public async Task<List<ChatMessage>> GetChatHistoryAsync(int userId, string sessionId)
        {
            return await _context.ChatMessages
                .Where(m => m.UserId == userId && m.SessionId == sessionId && !m.IsDeleted)
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();
        }

       
        public async Task<List<ChatRespons>> GetAllChatResponsesAsync()
        {
            return await _context.ChatResponses
                .OrderByDescending(r => r.Timestamp)
                .ToListAsync();
        }

        private async Task<string> CallTavilyAI(string userInput)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://api.tavily.com/");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "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");

                var body = new
                {
                    query = userInput,
                    search_depth = "basic",
                    include_answer = true,
                    include_raw_content = false,
                    max_results = 5
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync("search", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Tavily API error: {response.StatusCode} - {errorContent}");
                }

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("answer", out var answerElement) &&
                    !string.IsNullOrEmpty(answerElement.GetString()))
                {
                    return answerElement.GetString();
                }

                if (doc.RootElement.TryGetProperty("results", out var resultsElement) &&
                    resultsElement.GetArrayLength() > 0)
                {
                    var firstResult = resultsElement[0];
                    if (firstResult.TryGetProperty("content", out var contentElement))
                    {
                        return contentElement.GetString() ?? "No content available";
                    }
                }

                return "I'm sorry, I couldn't find a relevant answer to your question.";
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Tavily API Error: {ex.Message}");
                return "I'm experiencing technical difficulties. Please try again later.";
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
                return "I received an unexpected response. Please try again.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return "An unexpected error occurred. Please try again.";
            }
        }

        public async Task<ChatMessage?> EditMessageAsync(int userId, int messageId, EditMessageDto dto)
        {
            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && m.UserId == userId && !m.IsDeleted);

            if (message == null)
                return null;

            message.Message = dto.Message;
            message.Timestamp = DateTime.UtcNow; // Update timestamp to reflect edit time

            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<bool> SoftDeleteMessageAsync(int userId, int messageId)
        {
            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && m.UserId == userId && !m.IsDeleted);

            if (message == null)
                return false;

            message.IsDeleted = true;
            message.Timestamp = DateTime.UtcNow; // Update timestamp to reflect deletion time

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ChatMessage?> ApproveMessageAsync(int messageId, bool isApproved)
        {
            var message = await _context.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId && !m.IsDeleted);

            if (message == null)
                return null;

            message.IsApproved = isApproved;
            message.Timestamp = DateTime.UtcNow; // Update timestamp to reflect approval change

            await _context.SaveChangesAsync();
            return message;
        }
    }
}
