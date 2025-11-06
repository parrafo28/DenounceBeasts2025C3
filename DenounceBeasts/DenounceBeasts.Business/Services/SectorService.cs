using AutoMapper;
using DenounceBeasts.Business.Dtos;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Repositories;

namespace DenounceBeasts.API.Controllers
{

    public class SectorService
    {

        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SectorService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<SectorDto> GetAll()
        {
            var sectors = _unitOfWork.SectorRepository.GetAll();
            var response = _mapper.Map<List<SectorDto>>(sectors);

            return response;
        }


        public List<SectorDto> GetAllWithMunicipality()
        {
            var sectors = _unitOfWork.SectorRepository.GetAllWithMuniciplity();
            var response = _mapper.Map<List<SectorDto>>(sectors);
            var municipalities = _unitOfWork.MunicipalityRepository.GetAll();
            return response;
        }

        public SectorDto GetById(int id)
        {
            try
            {
                var sector = _unitOfWork.SectorRepository.GetById(id);
                var response = new SectorDto
                {
                    Id = sector.Id,
                    Name = sector.Name,
                    PostalCode = sector.PostalCode,
                    MunicipalityName = sector.Municipality?.Name
                };
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SectorDto Create(SectorDto request)
        {

            var sector = _mapper.Map<Sector>(request);

            sector.Id = _unitOfWork.SectorRepository.Create(sector);
            _unitOfWork.Complete();
            return request;
        }

        public void Update(int id, SectorDto updatedSector)
        {
            var sector = _unitOfWork.SectorRepository.GetById(id);
            if (sector == null)
            {
                throw new Exception("Sector not Found");
            }
            sector.PostalCode = updatedSector.PostalCode;
            sector.Name = updatedSector.Name;
            _unitOfWork.SectorRepository.Update(sector);
            _unitOfWork.Complete();

        }

        public void Delete(int id)
        {
            _unitOfWork.SectorRepository.Delete(id);
            _unitOfWork.Complete();
        }

    }
}
