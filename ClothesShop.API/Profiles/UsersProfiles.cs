using AutoMapper;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;

namespace ClothesShop.API.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            // Source => Destination

            // Use for read data (GET, GET single)
            CreateMap<User, UserDto>();
        }
    }
}
