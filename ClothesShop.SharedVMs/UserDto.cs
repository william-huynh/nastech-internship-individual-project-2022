using ClothesShop.SharedVMs.Enum;
using System.Text.Json.Serialization;

namespace ClothesShop.SharedVMs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public string Email { get; set; } = string.Empty;
        public int? CartId { get; set; }
    }
}
