using ClotheShop.API.Models;
using ClothesShop.API.Models.Enum;

namespace ClothesShop.API.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Roles { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
