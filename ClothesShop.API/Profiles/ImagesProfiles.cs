using AutoMapper;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;

namespace ClothesShop.API.Profiles
{
    public class ImagesProfiles : Profile
    {
        public ImagesProfiles()
        {
            // Source => Destination

            // Use for read data (GET, GET single)
            CreateMap<Image, ImageDto>();

            // Use for write data (POST, PUT)
            CreateMap<ImageDto, Image>()
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.IsDeleted, o => o.Ignore());
        }
    }
}
