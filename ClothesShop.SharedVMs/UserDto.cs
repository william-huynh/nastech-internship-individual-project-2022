using ClothesShop.SharedVMs.Enum;
using System.Text.Json.Serialization;

namespace ClothesShop.SharedVMs
{
    public class UserDto
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
    }
}
