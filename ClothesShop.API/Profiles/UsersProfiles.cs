using AutoMapper;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Authenticate;

namespace ClothesShop.API.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            // Source => Destination

            // Use for read data (GET, GET single)
            CreateMap<User, UserDto>();

            // Use for register
            CreateMap<RegisterRequestDto, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
