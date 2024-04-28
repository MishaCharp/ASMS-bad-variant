using ASMS.Library.Models;

namespace ASMS.Test.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<LoginResponse> Login(LoginRequest loginRequest);
        public Task<RegisterResponse> Register(RegisterRequest registerRequest);
    }
}
