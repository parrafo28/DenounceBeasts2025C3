using AutoMapper;
using DenounceBeasts.Business.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        private readonly SectorService _sectorService;

        //private readonly SectorRepository _repo;
        //private readonly MunicipalityRepository _municipaltyRepo;
        //private readonly UnitOfWork _unitOfWork;
        // private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SectorsController(//SectorRepository repo, MunicipalityRepository municipaltyRepo,
                                 // UnitOfWork unitOfWork,
                                 // ApplicationDbContext context, 
            SectorService sectorService,
            IMapper mapper)
        {
            this._sectorService = sectorService;
            //_repo = repo;
            //_municipaltyRepo = municipaltyRepo;
            //   _unitOfWork = unitOfWork;
            // _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<SectorDto>> GetAll()
        {
            return _sectorService.GetAll();
        }

        [HttpGet]
        [Route("WithMunicipality")]
        public ActionResult<List<SectorDto>> GetAllWithMunicipality()
        {

            return _sectorService.GetAllWithMunicipality();

        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<SectorDto> GetById(int id)
        {
            return _sectorService.GetById(id);
        }

        [HttpPost]
        public ActionResult<SectorDto> Create(SectorDto request)
        {

            var sector = _sectorService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, SectorDto updatedSector)
        {
            _sectorService.Update(id, updatedSector); 
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _sectorService.Delete(id); 
            return NoContent();
        }

    }
}
