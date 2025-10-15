using AutoMapper;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Entities;

namespace DenounceBeasts.API.Models
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

            ////CreateMap<SectorDto, Sector>();
            //CreateMap<Municipality, MunicipalityDto>()
            //   .ForMember(dest => dest.Sectors, opt => opt.Ignore()).ReverseMap();
        }
    }
}
