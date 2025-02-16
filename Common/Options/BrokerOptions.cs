namespace Common.Options
{
    public class BrokerOptions
    {
        public const string SectionName = "BrokerOptions";
        public string Host { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
