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
            CreateMap<List<CategoryDto>, CategoriesListDto>()
                .ForMember(dest => dest.Categories, act => act.MapFrom(src => src));

            // Use for write data (POST, PUT)
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.IsDeleted, o => o.Ignore());
        }
    }
}
