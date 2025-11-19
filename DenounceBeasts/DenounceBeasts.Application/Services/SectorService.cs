

using AutoMapper;
using DenounceBeasts.Application.Core;
using DenounceBeasts.Application.Dtos;
using DenounceBeasts.Application.Responses;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure;

namespace DenounceBeasts.Application.Services
{
    public class SectorService: BaseService
    {
        ////private readonly UnitOfWork UnitOfWork;
        ////private readonly IMapper Mapper;

        public SectorService(UnitOfWork unitOfWork, IMapper mapper): base(unitOfWork, mapper)  
        {
            //this.UnitOfWork = unitOfWork;
            //this.Mapper = mapper;
        }

        public ApiResponse<List<SectorDto>> GetAllSectors()
        {
            var sectors = UnitOfWork.SectorRepository.GetAll();

            var status = UnitOfWork.StatusRepository.GetAll();

            var selectedSectors = Mapper.Map<List<SectorDto>>(sectors);
            return ApiResponse<List<SectorDto>>.SuccessResponse(selectedSectors);
        }

        public ApiResponse<SectorDto> GetById(int id)
        {
            var sector = UnitOfWork.SectorRepository.GetById(id);

            if (sector == null) throw new KeyNotFoundException("Sector Not Found");

            return ApiResponse<SectorDto>.SuccessResponse(Mapper.Map<SectorDto>(sector));

        }

        public ApiResponse<SectorDto> GetSectorsByMunicipality(int id)
        {
            var sector = UnitOfWork.SectorRepository.GetSectorsByMunicipality(id);

            if (sector == null) throw new KeyNotFoundException("Sector Not Found"); 

            return ApiResponse<SectorDto>.SuccessResponse(Mapper.Map<SectorDto>(sector));

        }

        public ApiResponse<int> Create(SectorDto request)
        {
            var sectorAtDb = Mapper.Map<Sector>(request);
            var response = UnitOfWork.SectorRepository.Create(sectorAtDb);
            UnitOfWork.Complete();
            return ApiResponse<int>.SuccessResponse(response);

        }

        public void Update(int id, SectorDto updatedSector)
        {
            var sectorAtDb = Mapper.Map<Sector>(updatedSector);
            UnitOfWork.SectorRepository.Update(id, sectorAtDb);
            UnitOfWork.Complete(); 
        }

        public void Delete(int id)
        {
            UnitOfWork.SectorRepository.Delete(id);
            UnitOfWork.Complete(); 
        }
    }
}
