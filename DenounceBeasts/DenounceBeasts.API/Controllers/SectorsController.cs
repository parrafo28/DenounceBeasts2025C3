using AutoMapper;
using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.DTOs;
using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SectorsController(ApplicationDbContext context, IMapper mapper)
        {
            //_context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SectorDto>> GetAll()
        {
            var sectors = _context.Sectors.Include(p => p.Municipality)
                .ToList();

            // var sectorsResponse = new List<SectorDto>();
            //foreach (var sector in sectors)
            //{
            //    sectorsResponse.Add(new SectorDto
            //    {
            //        Id = sector.Id,
            //        //IsActive = sector.IsActive,
            //        PostalCode = sector.PostalCode,
            //        Name = sector.Name,
            //        MunicipalityId = sector.MunicipalityId, 
            //        MunicipalityName = sector.Municipality.Name
            //    });
            //}

            //sectorsResponse = sectors.Select(s => new SectorDto
            //{
            //    Id = s.Id,
            //    //IsActive = s.IsActive,
            //    PostalCode = s.PostalCode,
            //    Name = s.Name,
            //    MunicipalityId = s.MunicipalityId,
            //    //MunicipalityName = (s.Municipality == null) ? string.Empty: s.Municipality.Name,
            //    //MunicipalityName = (s.Municipality != null) ? s.Municipality.Name : string.Empty,
            //    MunicipalityName = s.Municipality?.Name

            //}).ToList();
            var sectorsResponse = _mapper.Map<List<SectorDto>>(sectors);
            return Ok(sectorsResponse);
        }

        //[HttpGet]
        //[Route("GetAllSector")]
        //public ActionResult<IEnumerable<Sector>> GetAllx()
        //{
        //    return Ok(sectors);
        //}

        //[HttpGet("GetAllSectorv2")]
        //public ActionResult<IEnumerable<Sector>> GetAllxx()
        //{
        //    return Ok(sectors);
        //}

        //[HttpGet("/GetAllSectorv3")]
        //public ActionResult<IEnumerable<Sector>> GetAllxxx()
        //{
        //    return Ok(sectors);
        //}
        // [HttpGet("{id:int}")]
        [HttpGet("{id}")]
        public ActionResult<SectorDto> GetById(int id)
        {
            var sector = _context.Sectors.Include(p=>p.Municipality)
                .FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }

            //var sectorDto = new SectorDto
            //{
            //    Id = sector.Id,
            //    //IsActive = sector.IsActive,
            //    PostalCode = sector.PostalCode,
            //    Name = sector.Name,
            //    MunicipalityId = sector.MunicipalityId,
            //    MunicipalityName = sector.Municipality.Name

            //};
           var sectorDto = _mapper.Map<SectorDto>(sector);
            return Ok(sectorDto);
        }

        [HttpPost]
        public ActionResult<CreateSectorResponse> Create(SectorDto request)
        {
            //var sector = new Sector
            //{
            //    //IsActive = request.IsActive,
            //    PostalCode = request.PostalCode,
            //    Name = request.Name,
            //    MunicipalityId = request.MunicipalityId
            //};

            var sector = _mapper.Map<Sector>(request);
            _context.Sectors.Add(sector);
            _context.SaveChanges();
            // request.Id = sector.Id;
            //  return CreatedAtAction(nameof(GetById), new { id = sector.Id }, sector);
            return Ok(new SectorDto { Id = sector.Id });
            // return Ok(request);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, SectorDto updatedSector)
        {
            var sector = _context.Sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.Name = updatedSector.Name;
            sector.PostalCode = updatedSector.PostalCode;
            //sector.IsActive = updatedSector.IsActive;
            sector.MunicipalityId = updatedSector.MunicipalityId;
            _context.Sectors.Update(sector);
            _context.SaveChanges();
            return NoContent();
            // return Ok(sectors);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var sector = _context.Sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            _context.Sectors.Remove(sector);
            _context.SaveChanges();
            return NoContent();
            // return Ok(sectors);
        }

    }
}
