using ASMS.Database.Models;
using ASMS.Database.Repositories;
using ASMS.Library.Models.LoginModels;
using ASMS.Library.Models.RegisterModels;
using ASMS.Server.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ASMS.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAllOrigins")]
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
            var user = Repositories.UserRepository
                .GetAll()
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
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                };
                var identity = new ClaimsIdentity(claims, "login");

                var generatedData = _jwtTokenHelper.GenerateJwtToken(identity);
                user.RefreshToken = generatedData.Item2;
                if (!Repositories.UserRepository.Update(user))
                {
                    return new LoginResponse
                    (
                        isSuccess: false,
                        responseText: "Произошла ошибка"
                    );
                }

                return new LoginResponse
                    (
                        isSuccess: true,
                        responseText: "Авторизация успешна. Обновлен refresh-token",
                        token: generatedData.Item1
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

            var userRole = Repositories.RoleRepository.GetAll().FirstOrDefault(x => x.Name == "User");
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

        [HttpGet]
        [Route("/[controller]/[action]")]
        public IActionResult Check()
        {
            string token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Токен авторизации не предоставлен");
            }

            try
            {
                // Декодируем и проверяем JWT-токен
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Проверяем, что токен не истек и имеет правильный секретный ключ
                if (jwtToken.ValidTo < DateTime.UtcNow || jwtToken.SignatureAlgorithm != "HS256")
                {
                    return Unauthorized("Недействительный или истекший токен авторизации");
                }

                // Возвращаем успешный ответ с информацией о пользователе

                var roleId = jwtToken.Claims.FirstOrDefault(x => x.Type == "role")?.Value;
                var userLogin = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name")?.Value;

                return Ok(new { roleId, userLogin });
            }
            catch
            {
                return Unauthorized("Недействительный токен авторизации");
            }
        }


    }
}
