using System.ComponentModel.DataAnnotations;

namespace ClothesShop.SharedVMs.Login
{
    public class LoginReadDto
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
