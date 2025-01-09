using System.Security.Cryptography;

namespace PracticeWebApp.Services
{
    public class PassProtector
    {
        private const int keySize = 64;
        private const int iterations = 400_000;
        public (string,string) Hash(string password,out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA512, keySize);
            return (Convert.ToBase64String(hash),Convert.ToBase64String(salt));
        }
    }
}
