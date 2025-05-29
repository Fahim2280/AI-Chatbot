using AI_Chatbot.DTOs;

namespace AI_Chatbot.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
        Task LogoutAsync();

    }
}
