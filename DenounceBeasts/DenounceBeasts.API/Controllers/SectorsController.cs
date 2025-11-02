using AutoMapper;
using DenounceBeasts.API.Models.DTOs;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;
using DenounceBeasts.Infrasctructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        //private readonly SectorRepository _sectorRepository;
        //private readonly MunicipalityRepository _municipalityRepository;
        private readonly UnitOfWork _unitOfWork;

        public SectorsController(ApplicationDbContext context, IMapper mapper,
            //SectorRepository sectorRepository, MunicipalityRepository municipalityRepository,
            UnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            //_sectorRepository = sectorRepository;
            //this._municipalityRepository = municipalityRepository;
            this._unitOfWork = unitOfWork;
            //_sectorRepository = new SectorRepository(_context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<SectorDto>> GetAll()
        {
            var sectors = _unitOfWork.SectorRepository.GetAll();
            //var sectors = _sectorRepository.GetAll();
            var sectorsResponse = MapFromSectorToSectorDto(sectors);
            return Ok(sectorsResponse);
        }


        [HttpGet]
        [Route("with-municipality")]
        public ActionResult<IEnumerable<SectorDto>> GetAllWithMunicipality()
        {
            var sectors = _unitOfWork.SectorRepository.GetAllWithMunicipality();
            var sectorsResponse = MapFromSectorToSectorDto(sectors);

            var municipalit = _unitOfWork.MunicipalityRepository.GetAll();
            return Ok(sectorsResponse);
        }

        [HttpGet("{id}")]
        public ActionResult<SectorDto> GetById(int id)
        {
            var sector = _unitOfWork.SectorRepository.GetById(id);
            if (sector == null)
            {
                return NotFound();
            }

            var sectorDto = _mapper.Map<SectorDto>(sector);
            return Ok(sectorDto);
        }

        [HttpPost]
        public ActionResult<CreateSectorResponse> Create(SectorDto request)
        {

            var sector = _mapper.Map<Sector>(request);
            var id = _unitOfWork.SectorRepository.Create(sector);
            _unitOfWork.Complete();

            //var idx = sector.Id;
            return Ok(new SectorDto { Id = id });
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, SectorDto updatedSector)
        {
            var sector = _unitOfWork.SectorRepository.GetById(id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.Name = updatedSector.Name;
            sector.PostalCode = updatedSector.PostalCode;
            sector.MunicipalityId = updatedSector.MunicipalityId;
            _unitOfWork.SectorRepository.Update(sector);
            //  _sectorRepository.SaveChanges();
            _unitOfWork.Complete();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _unitOfWork.SectorRepository.Delete(id);
            _unitOfWork.Complete();

            return NoContent();
        }

        private List<SectorDto> MapFromSectorToSectorDto(List<Sector> sectors)
        {
            return _mapper.Map<List<SectorDto>>(sectors);
        }

    }
}
