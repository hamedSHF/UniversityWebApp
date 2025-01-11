using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class RegisterRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
