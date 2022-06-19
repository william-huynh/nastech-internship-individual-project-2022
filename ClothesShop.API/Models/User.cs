using ClotheShop.API.Models;
using ClothesShop.API.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ClothesShop.API.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Roles { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
