using System.ComponentModel.DataAnnotations;

namespace shaker.domain.dto.Users
{
    public class AuthDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}