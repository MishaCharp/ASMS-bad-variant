using ASMS.Database.Models;
using ASMS.Database.Repositories;
using ASMS.Library.Models;
using ASMS.Server.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASMS.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AccountController(JwtTokenHelper jwtTokenHelper)
        {
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public LoginResponse Login(LoginRequest loginRequest)
        {
            var user = Repositories.UserRepository.
                GetWithInclude(x => x.Role)
                .FirstOrDefault(x=>x.Login == loginRequest.Login && x.Password == loginRequest.Password);
            
            if(user == null)
            {
                return new LoginResponse
                    (
                        isSuccess: false,
                        errorText: "Указанный пользователь не найден"
                    );
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Role.RoleName)
                };
                var identity = new ClaimsIdentity(claims, "login");

                var token = _jwtTokenHelper.GenerateJwtToken(identity);

                return new LoginResponse
                    (
                        isSuccess: true,
                        responseText: "Авторизация успешна. Обновлен refresh-token",
                        token: token
                    );
            }
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public RegisterResponse Register(RegisterRequest registerRequest)
        {
            if (!registerRequest.IsCorrectRequest)
            {
                return new RegisterResponse(false,registerRequest.GetErrorText());
            }

            var findedUser = Repositories.UserRepository.GetAll().FirstOrDefault(x => x.Login == registerRequest.Login);
            if(findedUser != null)
                return new RegisterResponse (false,"Пользователь с таким логином уже существует!");

            var userRole = Repositories.RoleRepository.GetAll().FirstOrDefault(x => x.RoleName == "User");
            if (userRole == null)
                return new RegisterResponse(false, "Роль пользователя не найдена! Ошибка БД!");

            var result = Repositories.UserRepository.Create(new User
            {
                Login = registerRequest.Login,
                Password = registerRequest.Password,
                Birthday = registerRequest.GetBirthday(),
                Role = userRole
            });

            return result   
                ? new RegisterResponse(true, "Пользователь успешно добавлен!")
                : new RegisterResponse(false, "Пользователь не добавлен!\nПроизошла ошибка!");

        }

        [Authorize]
        [HttpGet]
        [Route("/[controller]/[action]")]
        public IEnumerable<User> Get()
        {
            return Repositories.UserRepository.
                GetWithInclude(x => x.Role);
        }


    }
}
