using ClothesShop.SharedVMs.Enum;

namespace ClotheShop.SharedVMs.Users
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public int? CartId { get; set; }
    }
}
