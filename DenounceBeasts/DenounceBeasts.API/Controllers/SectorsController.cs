using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SectorsController(ApplicationDbContext context)
        {

            _context = context;
        }

        [HttpGet]
        public ActionResult<List<SectorDto>> GetAll()
        {
            var sectors = _context.Sectors.ToList();
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
            var selectedSectors = sectors.Select(s => new SectorDto
            {
                Id = s.Id,
                PostalCode = s.PostalCode,
                Name = s.Name,
                MunicipalityId = s.MunicipalityId
            }).ToList();

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
            var sector = _context.Sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            return Ok(sector);
        }

        [HttpPost]
        public ActionResult<SectorDto> Create(SectorDto sector)
        {
            //var sectorDb = new Sector();
            //sectorDb.PostalCode = sector.PostalCode;
            //sectorDb.Name = sector.Name;
            //sectorDb.MunicipalityId = sector.MunicipalityId;

            var sectorAtDb = new Sector
            {
                MunicipalityId = sector.MunicipalityId,
                PostalCode = sector.PostalCode,
                Name = sector.Name 
            };

            _context.Sectors.Add(sectorAtDb);
            _context.SaveChanges();
            return Ok(sectorAtDb.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Sector> Update(int id, Sector updatedSector)
        {
            var sector = _context.Sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.PostalCode = updatedSector.PostalCode;
            sector.Name = updatedSector.Name;
            _context.Sectors.Update(sector);
            _context.SaveChanges();
            return NoContent();
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
