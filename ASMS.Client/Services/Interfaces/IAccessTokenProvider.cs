namespace ASMS.Client.Services.Interfaces
{
    public interface IAccessTokenProvider
    {
        Task<string?> GetAccessTokenAsync();
        void SaveAccessToken(string token);
    }
}
