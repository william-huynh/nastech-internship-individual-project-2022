using System.ComponentModel.DataAnnotations;

namespace ClothesShop.SharedVMs.Authenticate
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
