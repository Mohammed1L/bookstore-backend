using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookstoreDB.Data;
using BookStoreBackend.Models;   
using BookStoreBackend.Dtos;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace services
{
    public class TokenServices
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenServices(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }





        public string GenerateToken(String UserId, string role)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier,UserId ),
                    new Claim(ClaimTypes.Role, role)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:DurationInMinutes"])),
                signingCredentials: sign
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GetEmailFromToken()
        {
            
            var user = _httpContextAccessor.HttpContext?.User;
            foreach (var claim in user.Claims)
{
       Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
}
            if (user == null || !user.Identity.IsAuthenticated)
            {
                Console.WriteLine("User is not auth");
                return null;
            }
            var email = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return email;
        }
    }
}