using AutoMapper;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Models.Command, Dtos.CommandReadDto>();
            CreateMap<Dtos.CommandCreateDto, Models.Command>();
            CreateMap<Models.Platform, Dtos.PlatformReadDto>();
            CreateMap<Dtos.PlatformPublishedDto, Models.Platform>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
            CreateMap<PlatformService.GrpcPlatformModel, Models.Platform>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId));
        }
    }
}
