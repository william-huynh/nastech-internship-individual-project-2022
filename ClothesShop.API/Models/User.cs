using ClothesShop.API.Models;
using ClothesShop.API.Models.Enum;
using System.Text.Json.Serialization;

namespace ClothesShop.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public Role Role { get; set; } 
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public int? CartId { get; set; }

        public virtual Cart Carts { get; set; } 
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
