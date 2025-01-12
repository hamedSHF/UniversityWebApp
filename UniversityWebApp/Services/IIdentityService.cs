using UniversityWebApp.Services.States;

namespace UniversityWebApp.Services
{
    public interface IIdentityService
    {
        public Task<Response> CreateUserForIdentity(string id,string userName,string password, string address);
    }
}
