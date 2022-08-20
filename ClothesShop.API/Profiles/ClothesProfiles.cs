using AutoMapper;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;

namespace ClothesShop.API.Profiles
{
    public class ClothesProfiles : Profile
    {
        public ClothesProfiles()
        {
            // Source => Destination

            // Use for read data (GET, GET single)
            CreateMap<Clothes, ClothesDto>();
            CreateMap<List<ClothesDto>, ClothesListDto>()
                .ForMember(dest => dest.Clothes, act => act.MapFrom(src => src));

            // Use for write data (POST, PUT)
            CreateMap<ClothesDto, Clothes>()
                .ForMember(dest => dest.AddedDate, o => o.Ignore())
                .ForMember(dest => dest.IsDeleted, o => o.Ignore());
        }
    }
}
