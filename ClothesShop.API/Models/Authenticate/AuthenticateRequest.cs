using System.ComponentModel.DataAnnotations;

namespace ClothesShop.API.Models.Authenticate
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
