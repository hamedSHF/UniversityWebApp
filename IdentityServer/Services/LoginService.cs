using Azure.Core;
using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenGenerator tokenGenerator;
        public LoginService(SignInManager<ApplicationUser> signInManager,
            ITokenGenerator tokenGenerator)
        {
            this.signInManager = signInManager;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponseBody> Login(LoginRequestBody request, string role)
        {
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await signInManager.UserManager.FindByNameAsync(request.UserName);
                var response = new LoginResponseBody
                {
                    Content = user.Id,
                    JWTToken = this.tokenGenerator.GenerateJwtToken(this.tokenGenerator.GenerateJwtClaims(user.Id, role)),
                    ResponseState = ResponseState.OK
                };
                return response;
            }
            else if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
            {
                return new LoginResponseBody
                {
                    ResponseState = ResponseState.NotFound
                };
            }
            return new LoginResponseBody
            {
                ResponseState = ResponseState.Failed
            };
        }
    }
}
