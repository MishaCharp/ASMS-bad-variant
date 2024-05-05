using ASMS.Database.Models;
using ASMS.Database.Repositories;
using ASMS.Library.Models.LoginModels;
using ASMS.Library.Models.ProfileModels;
using ASMS.Server.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ASMS.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class ProfileController : ControllerBase
    {
        public ProfileController()
        {
            
        }

        [Authorize]
        [HttpPost]
        [Route("/[controller]/[action]")]
        public ProfileResponse EditProfile(ProfileRequest profileRequest)
        {
            if (!profileRequest.IsCorrectRequest)
            {
                return new ProfileResponse(false, errorText: profileRequest.ErrorText);
            }

            var user = Repositories.UserRepository
            .GetAll()
            .FirstOrDefault(x => x.Id == profileRequest.IdUser);

            if(user == null)
            {
                return new ProfileResponse(false, errorText: "Произошла ошибка изменения данных");
            }

            UpdateUserData(profileRequest,ref user);

            if (!Repositories.UserRepository.Update(user))
            {
                return new ProfileResponse
                    (
                        isSuccess: false,
                        responseText: "Произошла ошибка"
                    );
            }

            return new ProfileResponse
                    (
                        isSuccess: true,
                        responseText: "Профиль успешно обновлён"
                    );
        }

        private void UpdateUserData(ProfileRequest profileRequest,ref User user)
        {
            user.Login = profileRequest?.Login;
            user.Password = profileRequest?.Password;
            user.Birthday = profileRequest.Birthday;
        }

    }
}
