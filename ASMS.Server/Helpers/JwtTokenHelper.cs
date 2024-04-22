using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ASMS.Server.Helpers
{
    public class JwtTokenHelper
    {
        private readonly IConfiguration _configuration;

        public JwtTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Метод, генерирующий jwt-токен и refresh-токен
        /// </summary>
        /// <param name="identity"></param>
        /// <returns>1.string - jwt-токен | 2.string - refresh-токен</returns>
        public (string, string) GenerateJwtToken(ClaimsIdentity identity)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpirationDays"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = GenerateRefreshToken();
            var resultToken = tokenHandler.WriteToken(token);

            return (resultToken, refreshToken);
        }

        public string GenerateRefreshToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenBytes = new byte[64];
                rng.GetBytes(tokenBytes);

                return Convert.ToBase64String(tokenBytes);
            }
        }
    }
}
