using UniversityWebApp.Services.Registration;

namespace UniversityWebApp.Services.Registration
{
    public interface IRegistrationService<T>
    {
        public Task<RegistrationResult> RegisterUser(T user);
    }
}
