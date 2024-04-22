using ASMS.Client.Services.Interfaces;
using ASMS.Library.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using LoginRequest = ASMS.Library.Models.LoginRequest;

namespace ASMS.Client.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly HttpClient _httpClient;

        public AuthorizationService(NavigationManager navigationManager, IAccessTokenProvider accessTokenProvider, HttpClient httpClient)
        {
            _navigationManager = navigationManager;
            _accessTokenProvider = accessTokenProvider;
            _httpClient = httpClient;
        }

        public string Authorize(string login, string password)
        {
            var request = new LoginRequest(login, password);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string resultText = "";

            var response = _httpClient.PostAsync(Options.SERVER_URL + "Account/Login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseJson = response.Content.ReadAsStringAsync().Result;
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseJson);

                if (loginResponse.IsSuccess)
                {
                    _accessTokenProvider.SaveAccessToken(loginResponse.Token);
                    _navigationManager.NavigateTo("/");
                }
                else resultText = loginResponse.ErrorText;
            }

            //_navigationManager.NavigateTo("/login");
            return resultText;
        }
    }
}
