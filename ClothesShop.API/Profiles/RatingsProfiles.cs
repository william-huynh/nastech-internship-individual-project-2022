using AutoMapper;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;

namespace ClothesShop.API.Profiles
{
    public class RatingsProfiles : Profile
    {
        public RatingsProfiles()
        {
            // Source => Destination

            // Use for read data (GET, GET single)
            CreateMap<Rating, RatingDto>();

            // Use for write data (POST, PUT)
            CreateMap<RatingDto, Rating>()
                .ForMember(dest => dest.IsDelete, o => o.Ignore());
        }
    }
}
