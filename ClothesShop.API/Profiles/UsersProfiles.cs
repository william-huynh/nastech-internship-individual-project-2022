using AutoMapper;
using ClotheShop.API.Models;
using ClotheShop.SharedVMs.Users;

namespace ClothesShop.API.Profiles
{
    public class UsersProfiles : Profile
    {
        public UsersProfiles()
        {
            // Source => Destination

            // Use for read data (GET, GET single)
            CreateMap<User, UserReadDto>();
        }
    }
}
