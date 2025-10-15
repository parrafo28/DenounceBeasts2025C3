using AutoMapper;
using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SectorsController(ApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<SectorDto>> GetAll()
        {
            var sectors = _context.Sectors//.Include(p=> p.Municipality)
                .AsNoTracking().ToList();
            var list = new List<SectorDto>();
            //foreach (var sector in sectors)
            //{
            //    list.Add(new SectorDto
            //    {
            //        Id = sector.Id,
            //        PostalCode = sector.PostalCode,
            //        Name = sector.Name,
            //        MunicipalityId = sector.MunicipalityId
            //    });
            //}
            //foreach (var sector in sectors)
            //{
            //    sector.PostalCode = string.Empty;
            //    _context.Sectors.Update(sector);
            //}

            //_context.SaveChanges();
            var selectedSectors = sectors.Select(s => new SectorDto
            {
                Id = s.Id,
                PostalCode = s.PostalCode,
                Name = s.Name,
                MunicipalityId = s.MunicipalityId,
                MunicipalityName = s.Municipality?.Name
            }).ToList();
           // var selectedSectors = _mapper.Map<List<SectorDto>>(sectors);
            return Ok(selectedSectors);
        }

        //[HttpGet]
        //[Route("GetSectors")]
        //public ActionResult<List<Sector>> GetAllx()
        //{
        //    return Ok(sectors);
        //}

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Sector> GetById(int id)
        {
            var sector = _context.Sectors
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            return Ok(sector);
        }

        [HttpPost]
        public ActionResult<SectorDto> Create(SectorDto request)
        {
            //var sectorDb = new Sector();
            //sectorDb.PostalCode = sector.PostalCode;
            //sectorDb.Name = sector.Name;
            //sectorDb.MunicipalityId = sector.MunicipalityId;
                        
            //var sectorAtDb = new Sector
            //{
            //    MunicipalityId = sector.MunicipalityId,
            //    PostalCode = sector.PostalCode,
            //    Name = sector.Name
            //};
            var sectorAtDb = _mapper.Map<Sector>(request);
            _context.Sectors.Add(sectorAtDb);
            _context.SaveChanges();
            return Ok(sectorAtDb.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Sector> Update(int id, Sector updatedSector)
        {
            var sector = _context.Sectors.FirstOrDefault(s => s.Id == id);

            if (sector != null)
            {
                sector.PostalCode = updatedSector.PostalCode;
                sector.Name = updatedSector.Name;
                _context.Sectors.Update(sector);
                _context.SaveChanges();
                return NoContent();
            }

            return NotFound();

        }
        [HttpDelete]
        [Route("{id}")]
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
        }


    }
}
