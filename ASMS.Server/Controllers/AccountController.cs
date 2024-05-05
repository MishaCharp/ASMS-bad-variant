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

            var userRole = Repositories.RoleRepository.GetAll().FirstOrDefault(x => x.Name == "User");
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

        [HttpGet]
        [Route("/[controller]/[action]")]
        public IActionResult Check()
        {
            string token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("����� ����������� �� ������������");
            }

            try
            {
                // ���������� � ��������� JWT-�����
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // ���������, ��� ����� �� ����� � ����� ���������� ��������� ����
                if (jwtToken.ValidTo < DateTime.UtcNow || jwtToken.SignatureAlgorithm != "HS256")
                {
                    return Unauthorized("���������������� ��� �������� ����� �����������");
                }

                // ���������� �������� ����� � ����������� � ������������

                var roleId = jwtToken.Claims.FirstOrDefault(x => x.Type == "role")?.Value;
                var userLogin = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name")?.Value;

                return Ok(new { roleId, userLogin });
            }
            catch
            {
                return Unauthorized("���������������� ����� �����������");
            }
        }


    }
}
