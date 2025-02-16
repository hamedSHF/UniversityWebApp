namespace UniversityWebApp.ConfigOptions
{
    public class IdentityAddressesOptions
    {
        public const string IdentityAddresses = "IdentityAddresses";
        public string IdentityServerSecure { get; set; } = null!;
        public string IdentityServerInsecure { get; set; } = null!;
        public StudentAddresses StudentAddresses { get; set; } = null!;
    }
    public class StudentAddresses
    {
        public string Register { get; set; } = null!;
    }
}
