using finalinternshipproject.Models;

namespace finalinternshipproject.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<int>> Register(string username, string password);
        Task<bool> UserExists(string username);
        Task<ServiceResponse<string>> UserSession();

        String CreateToken(User user);

        string GetUserName();
        ServiceResponse<int> GetUserId();
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    }
}
