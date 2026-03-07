using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.services
{
    public class JWTservice
    {
        private readonly IConfiguration _config;

        public JWTservice(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Guid id, string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Email, email),
            };


            Console.Write($"id: '{id}'");


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );
            string abc = new JwtSecurityTokenHandler().WriteToken(token);
            Debug.WriteLine(abc);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
