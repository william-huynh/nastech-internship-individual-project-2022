using ClothesShop.API.Models;
using ClothesShop.API.Models.Enum;
using System.Text.Json.Serialization;

namespace ClothesShop.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; } 
        public string Name { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public string Email { get; set; } = string.Empty;
        public int? CartId { get; set; }

        public virtual Cart Carts { get; set; } 
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
