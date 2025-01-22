namespace UniversityWebApp.ConfigOptions
{
    public class IdentityAddressesOptions
    {
        public const string IdentityAddresses = "IdentityAddresses";
        public string IdentityServerSecure { get; set; } = string.Empty;
        public string IdentityServerInsecure { get; set; } = string.Empty;
        public StudentAddresses StudentAddresses { get; set; }
    }
    public class StudentAddresses
    {
        public string Register { get; set; }
    }
}
