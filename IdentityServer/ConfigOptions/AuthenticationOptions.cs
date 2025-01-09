﻿namespace IdentityServer.ConfigOptions
{
    public class AuthenticationOptions
    {
        public const string Authentication = "Authentication";
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer {  get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;    
        public double Expiration { get; set; }
    }
}
