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
            CreateMap<Category, CategoriesReadDto>();
        }
    }
}
