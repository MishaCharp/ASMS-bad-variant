using ASMS.Library.Models;
using ASMS.Test.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace ASMS.Test.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var json = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Options.SERVER_URL + "Account/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                return loginResponse;
            }

            throw new Exception("Failed to login");
        }

        public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
        {
            var json = JsonConvert.SerializeObject(registerRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Options.SERVER_URL + "Account/Register", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var registerResponse = JsonConvert.DeserializeObject<RegisterResponse>(responseContent);
                return registerResponse;
            }

            throw new Exception("Failed to register");
        }
    }
}
