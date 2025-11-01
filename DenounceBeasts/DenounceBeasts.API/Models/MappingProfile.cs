using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.Domain.Entities;

namespace DenounceBeasts.API.Models
{
    public class MappingProfile : AutoMapper.Profile
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

            // CreateMap<SectorDto, Sector>();
            //CreateMap<Sector, SectorDto>();
            #endregion

            ////CreateMap<SectorDto, Sector>();
            //CreateMap<Municipality, MunicipalityDto>()
            //   .ForMember(dest => dest.Sectors, opt => opt.Ignore()).ReverseMap();
        }
    }
}