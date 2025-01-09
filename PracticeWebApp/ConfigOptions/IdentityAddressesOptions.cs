namespace UniversityWebApp.ConfigOptions
{
    public class IdentityAddressesOptions
    {
        public const string IdentityAddresses = "ExternalAddresses";
        public string IdentityServerSecure { get; set; } = string.Empty;
        public string IdentityServerInsecure { get; set; } = string.Empty;
    }
}
