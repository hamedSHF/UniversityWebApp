using IdentityServer.ConfigOptions;
using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthenticationOptions options;  
        public TokenGenerator(IOptions<AuthenticationOptions> options)
        {
            this.options = options.Value;
        }

        public IEnumerable<Claim> GenerateJwtClaims(string userId,string role)
        {
            return new List<Claim>()
            {
                new Claim("UserId",userId),
                new Claim("Role",role)
            };
        }

        public string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            var sigingCredentials = new SigningCredentials(symmetricKey,SecurityAlgorithms.HmacSha256);
            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                options.Issuer,
                null,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(options.Expiration),
                sigingCredentials
                ));
        }
    }
}
