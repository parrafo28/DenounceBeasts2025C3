using AutoMapper;
using DenounceBeasts.Application.DTOs;
using DenounceBeasts.Application.Services;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Repositories;

namespace DenounceBeasts.API.Controllers
{
    public class SectorService : BaseService
    {
        //private readonly IMapper Mapper;
        //private readonly UnitOfWork UnitOfWork;

        public SectorService(IMapper mapper, UnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            //Mapper = mapper;
            //UnitOfWork = unitOfWork;
        }

        public IEnumerable<SectorDto> GetAll()
        {
            var sectors = UnitOfWork.SectorRepository.GetAll();
            var sectorsResponse = MapFromSectorToSectorDto(sectors);
            return sectorsResponse;
        }
         
        public IEnumerable<SectorDto> GetAllSectorsWithMunicipality()
        {
            var sectors = UnitOfWork.SectorRepository.GetAllWithMunicipality();
            var sectorsResponse = MapFromSectorToSectorDto(sectors);

            var municipalit = UnitOfWork.MunicipalityRepository.GetAll();
            return sectorsResponse;
        }

        public SectorDto GetById(int id)
        {
            Sector sector = GetEntityById(id); 
            var sectorDto = Mapper.Map<SectorDto>(sector);
            return sectorDto;
        }

        public CreateSectorResponse Create(SectorDto request)
        { 
            var sector = Mapper.Map<Sector>(request);
            UnitOfWork.SectorRepository.Create(sector);
            UnitOfWork.Complete(); 
            return new CreateSectorResponse { Id = sector.Id };
        }

        public void Update(int id, SectorDto updatedSector)
        {
            Sector sector = GetEntityById(id);
            sector.Name = updatedSector.Name;
            sector.PostalCode = updatedSector.PostalCode;
            sector.MunicipalityId = updatedSector.MunicipalityId;
            UnitOfWork.SectorRepository.Update(sector);
            UnitOfWork.Complete();
        }

        public void Delete(int id)
        {
            UnitOfWork.SectorRepository.Delete(id);
            UnitOfWork.Complete();
        }

        private Sector GetEntityById(int id)
        {
            var sector = UnitOfWork.SectorRepository.GetById(id);
            if (sector == null)
            {
                throw new KeyNotFoundException("Sector Not Fount");
            } 
            return sector;
        }

        private List<SectorDto> MapFromSectorToSectorDto(List<Sector> sectors)
        {
            return Mapper.Map<List<SectorDto>>(sectors);
        } 
    }
}
