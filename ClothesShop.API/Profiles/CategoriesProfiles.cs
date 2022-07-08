using AutoMapper;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;

namespace ClothesShop.API.Profiles
{
    public class CategoriesProfiles : Profile
    {
        public CategoriesProfiles()
        {
            // Source => Destination

            // Use for read data (GET, GET single)
            CreateMap<Category, CategoryDto>();

            // Use for write data (POST, PUT)
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.IsDeleted, o => o.Ignore());
        }
    }
}
