using System.Security.Claims;

namespace IdentityServer.Services.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateJwtToken(IEnumerable<Claim> cliams);
        public IEnumerable<Claim> GenerateJwtClaims(string userId,string role);
    }
}
