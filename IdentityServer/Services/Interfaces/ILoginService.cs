using IdentityServer.Models;

namespace IdentityServer.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<LoginResponseBody> Login(LoginRequestBody requestBody,string role);
    }
}
