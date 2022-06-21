using AutoMapper;
using ClotheShop.API.Models;
using ClothesShop.API.DataTransferObject.Categories;

namespace ClothesShop.API.Profiles
{
    public class CategoriesProfiles : Profile
    {
        public CategoriesProfiles()
        {
            // Source => Destination

            // Use for read data (GET, GET single)
            CreateMap<Category, CategoriesReadDto>();

            // Use for write data (POST)
            CreateMap<CategoriesCreateDto, Category>();

            // Use for update data (PUT)
            CreateMap<CategoriesUpdateDto, Category>();
        }
    }
}
