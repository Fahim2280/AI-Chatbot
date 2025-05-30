using AI_Chatbot.DTOs;
using AI_Chatbot.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AI_Chatbot.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send(ChatMessageDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _chatService.SendMessageAsync(userId, dto);
            return Ok(response);
        }

        [HttpGet("history")]
        public async Task<IActionResult> History(string sessionId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var messages = await _chatService.GetChatHistoryAsync(userId, sessionId);
            return Ok(messages);
        }

        [HttpGet("responses")]
        public async Task<IActionResult> GetAllResponses()
        {
            var responses = await _chatService.GetAllChatResponsesAsync();
            return Ok(responses);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMessage(int id, [FromBody] EditMessageDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _chatService.EditMessageAsync(userId, id, dto);

                if (result == null)
                    return NotFound("Message not found or you don't have permission to edit it.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _chatService.SoftDeleteMessageAsync(userId, id);

                if (!result)
                    return NotFound("Message not found or you don't have permission to delete it.");

                return Ok(new { message = "Message deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> ApproveMessage(int id, [FromBody] ApproveMessageDto dto)
        {
            try
            {
                var result = await _chatService.ApproveMessageAsync(id, dto.IsApproved);

                if (result == null)
                    return NotFound("Message not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }


}