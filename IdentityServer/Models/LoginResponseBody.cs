using Microsoft.Identity.Client;

namespace IdentityServer.Models
{
    public class LoginResponseBody
    {
        public string Content { get; set; }
        public string JWTToken { get; set; }
        public ResponseState ResponseState { get; set; }
    }
    public enum ResponseState
    {
        OK,
        NotFound,
        Failed,
    }
}
