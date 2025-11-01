using AutoMapper;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using DenounceBeasts.Infrasctructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        //private readonly SectorRepository _repo;
        //private readonly MunicipalityRepository _municipaltyRepo;
        private readonly UnitOfWork _unitOfWork;
       // private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SectorsController(//SectorRepository repo, MunicipalityRepository municipaltyRepo,
            UnitOfWork unitOfWork,
           // ApplicationDbContext context, 
            IMapper mapper)
        {
            //_repo = repo;
            //_municipaltyRepo = municipaltyRepo;
            _unitOfWork = unitOfWork;
           // _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<SectorDto>> GetAll()
        {
            var sectors = _unitOfWork.SectorRepository.GetAll();
            var response = _mapper.Map<List<SectorDto>>(sectors);

            return Ok(response);
        }

        [HttpGet]
        [Route("WithMunicipality")]
        public ActionResult<List<SectorDto>> GetAllWithMunicipality()
        {
            var sectors = _unitOfWork.SectorRepository.GetAllWithMuniciplity();
            var response = _mapper.Map<List<SectorDto>>(sectors);
            var municipalities = _unitOfWork.MunicipalityRepository.GetAll(); 
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<SectorDto> GetById(int id)
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
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult<SectorDto> Create(SectorDto request)
        {

            var sector = _mapper.Map<Sector>(request);

            sector.Id = _unitOfWork.SectorRepository.Create(sector);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, SectorDto updatedSector)
        {
            var sector = _unitOfWork.SectorRepository.GetById(id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.PostalCode = updatedSector.PostalCode;
            sector.Name = updatedSector.Name;
            _unitOfWork.SectorRepository.Update(sector);
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

    }
}
