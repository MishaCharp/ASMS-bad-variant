using ASMS.Library.Models.LoginModels;
using ASMS.Library.Models.RegisterModels;

namespace ASMS.Client.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<LoginResponse> Login(LoginRequest loginRequest);
        public Task<RegisterResponse> Register(RegisterRequest registerRequest);
    }
}
