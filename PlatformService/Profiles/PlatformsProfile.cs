using AutoMapper;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Models.Platform, Dtos.PlatformReadDto>();
            CreateMap<Dtos.PlatformCreateDto, Models.Platform>();
            CreateMap<Dtos.PlatformReadDto, Dtos.PlatformPublishedDto>();
        }
    }
}
