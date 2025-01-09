using UniversityWebApp.Services.States;

namespace UniversityWebApp.Services
{
    public interface IAuthorizationService
    {
        public Task<Response> AuthorizeUserById(string userId, string address);
    }
}
