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
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<SectorDto>> GetAll()
        {
            var sectors = _context.Sectors
                .Include(p => p.Municipality)//.ThenInclude(p => p.Sectors)
                .ToList();
            //var sectorsREsponse = sectors.Select(s => new SectorDto
            //{
            //    Id = s.Id,
            //    Name = s.Name,
            //    PostalCode = s.PostalCode,
            //    MunicipalityName2 = s.Municipality == null ? string.Empty : s.Municipality.Name,
            //    MunicipalityName = s.Municipality?.Name
            //}).ToList();
            //var responses = new List<SectorDto>();
            //foreach (var item in sectors)
            //{
            //    var temp = new SectorDto
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        PostalCode = item.PostalCode
            //    };
            //    responses.Add(temp);

            //    var temp2 = new SectorDto();
            //    temp2.Id = item.Id;
            //    temp2.Name = item.Name;
            //    temp2.PostalCode = item.PostalCode;
            //    responses.Add(temp2); 

            //}

            var response = _mapper.Map<List<SectorDto>>(sectors);

            return Ok(response);
            //return Ok(sectorsREsponse);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<SectorDto> GetById(int id)
        {
            try
            { 
                var sector = _context.Sectors.FirstOrDefault(s => s.Id == id);
                if (sector == null)
                {
                    return NotFound();
                }
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
            //var sector = new Sector
            //{
            //    Name = request.Name,
            //    PostalCode = request.PostalCode  ,
            //    MunicipalityId = request.MunicipalityId
            //};

            var sector = _mapper.Map<Sector>(request);

            _context.Sectors.Add(sector);
            _context.SaveChanges();
            request.Id = sector.Id;
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, SectorDto updatedSector)
        {
            var sector = _context.Sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.PostalCode = updatedSector.PostalCode;
            sector.Name = updatedSector.Name;
            //  sector.IsActive = updatedSector.IsActive;
            _context.Sectors.Update(sector);
            _context.SaveChanges();
            return NoContent();
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

            return NoContent();
        }

    }
}
