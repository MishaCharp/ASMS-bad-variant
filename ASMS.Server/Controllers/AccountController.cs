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
            var user = Repositories.UserRepository
                .GetAll()
                .FirstOrDefault(x=>x.Login == loginRequest.Login && x.Password == loginRequest.Password);
            
            if(user == null)
            {
                return new LoginResponse
                    (
                        isSuccess: false,
                        errorText: "��������� ������������ �� ������"
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
                        responseText: "��������� ������"
                    );
                }

                return new LoginResponse
                    (
                        isSuccess: true,
                        responseText: "����������� �������. �������� refresh-token",
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
                return new RegisterResponse (false,"������������ � ����� ������� ��� ����������!");

            var userRole = Repositories.RoleRepository.GetAll().FirstOrDefault(x => x.RoleName == "User");
            if (userRole == null)
                return new RegisterResponse(false, "���� ������������ �� �������! ������ ��!");

            var result = Repositories.UserRepository.Create(new User
            {
                Login = registerRequest.Login,
                Password = registerRequest.Password,
                Birthday = registerRequest.GetBirthday(),
                Role = userRole
            });

            return result   
                ? new RegisterResponse(true, "������������ ������� ��������!")
                : new RegisterResponse(false, "������������ �� ��������!\n��������� ������!");

        }

        [Authorize]
        [HttpGet]
        [Route("/[controller]/[action]")]
        public void CheckAuthorized()
        {
            
        }


    }
}
