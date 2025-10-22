using AutoMapper;
using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Entities;
using DenounceBeasts.API.Models.Responses;
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
        public ApiResponse<List<SectorDto>> GetAll()
        {
            var sectors = _context.Sectors.Include(p => p.Municipality)
                .AsNoTracking().ToList();

            var selectedSectors = _mapper.Map<List<SectorDto>>(sectors);
            return ApiResponse<List<SectorDto>>.SuccessResponse(selectedSectors);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ApiResponse<SectorDto>> GetById(int id)
        {
            var sector = _context.Sectors.AsNoTracking().FirstOrDefault(s => s.Id == id);

            if (sector == null) return NotFound();

            return ApiResponse<SectorDto>.SuccessResponse(_mapper.Map<SectorDto>(sector));

        }

        [HttpPost]
        public ActionResult<ApiResponse<int>> Create(SectorDto request)
        {
            var sectorAtDb = _mapper.Map<Sector>(request);
            _context.Sectors.Add(sectorAtDb);
            _context.SaveChanges();
            return ApiResponse<int>.SuccessResponse(sectorAtDb.Id);

        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, SectorDto updatedSector)
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
