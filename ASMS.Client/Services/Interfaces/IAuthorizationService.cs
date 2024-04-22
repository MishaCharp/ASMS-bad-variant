using System.Security.Claims;

namespace ASMS.Client.Services.Interfaces
{
    public interface IAuthorizationService
    {
        string Authorize(string login, string password);

    }
}
