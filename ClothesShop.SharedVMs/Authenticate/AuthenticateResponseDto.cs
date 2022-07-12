using ClothesShop.SharedVMs.Enum;
using ClothesShop.SharedVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.SharedVMs.Authenticate
{
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseDto(UserDto user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Role = user.Role;
            Name = user.Name;
            Phone = user.Phone;
            Address = user.Address;
            IsDeleted = user.IsDeleted;
            Email = user.Email;
            Token = token;
        }
    }
}
