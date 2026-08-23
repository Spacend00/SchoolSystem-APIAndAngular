
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolSystem.Application.Common.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolSystem.Infrastructure.Services
{
    public class TokenService<T> : ITokenService<T> where T : IUserEntity
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration) => _configuration = configuration;
        public async Task<string> GenerateToken(T entity)
        {
            var claims = new List<Claim> 
            {
                new(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                new(ClaimTypes.Email, entity.Email),
                new(ClaimTypes.Role, entity.Role.ToString())
            };
            if(entity is Student student)
            {
                claims.Add(new Claim("SchoolNumber", student.SchoolNumber));
            }
            if(entity is Teacher teacher)
            {
                claims.Add(new Claim("Branch", teacher.Branch.ToString()));
            }
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
