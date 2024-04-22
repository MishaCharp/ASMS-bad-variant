using ASMS.Client.Services.Interfaces;

namespace ASMS.Client.Services
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        private readonly StorageService _storageService;

        public AccessTokenProvider(StorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<string?> GetAccessTokenAsync() => await _storageService.GetAsync<string>("access_token");

        public async void SaveAccessToken(string token) => await _storageService.SetAsync("access_token", token);
    }
}
