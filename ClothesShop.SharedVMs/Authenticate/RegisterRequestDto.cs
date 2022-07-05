using System.ComponentModel.DataAnnotations;

namespace ClothesShop.SharedVMs.Authenticate
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
    }
}