using AutoMapper;
using DenounceBeasts.Application.Dtos;
using DenounceBeasts.Domain.Entities;

namespace DenounceBeasts.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Sector
            CreateMap<Sector, SectorDto>()
                .ForMember(dest => dest.MunicipalityName,
                opt => opt.MapFrom(src => src.Municipality.Name))
                .ReverseMap()
                .ForMember(dest => dest.Municipality, opt => opt.Ignore());

            CreateMap<Sector, CreateSectorDto>()
                .ReverseMap();

            #endregion 
        }
    }
}
