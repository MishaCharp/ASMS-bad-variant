using ASMS.Database.Models;
using ASMS.Database.Repositories;
using ASMS.Library.Models.ProfileModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASMS.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        public ProfileController()
        {

        }

        [Authorize]
        [HttpPost]
        [Route("/[controller]/[action]")]
        public ProfileEditResponse EditProfile(ProfileEditRequest profileRequest)
        {
            if (!profileRequest.IsCorrectRequest)
            {
                return new ProfileEditResponse(false, errorText: profileRequest.ErrorText);
            }

            var user = Repositories.UserRepository
            .GetAll()
            .FirstOrDefault(x => x.Id == profileRequest.IdUser);

            if (user == null)
            {
                return new ProfileEditResponse(false, errorText: "Произошла ошибка изменения данных");
            }

            UpdateUserData(profileRequest, ref user);

            if (!Repositories.UserRepository.Update(user))
            {
                return new ProfileEditResponse
                    (
                        isSuccess: false,
                        responseText: "Произошла ошибка"
                    );
            }

            return new ProfileEditResponse
                    (
                        isSuccess: true,
                        responseText: "Профиль успешно обновлён"
                    );
        }

        [Authorize]
        [HttpGet]
        [Route("/[controller]/[action]")]
        public ProfileDto GetProfile()
        {
            var name = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var user = Repositories.UserRepository
            .GetAll()
            .FirstOrDefault(x => x.Login == name);
            
            if(user != null)
            {
                return new ProfileDto
                {
                    Login = user.Login,
                    Password = user.Password,
                    Birthday = user.Birthday,
                };
            }

            return null;

        }

        private void UpdateUserData(ProfileEditRequest profileRequest, ref User user)
        {
            user.Login = profileRequest?.Login;
            user.Password = profileRequest?.Password;
            user.Birthday = profileRequest.Birthday;
        }

    }
}
