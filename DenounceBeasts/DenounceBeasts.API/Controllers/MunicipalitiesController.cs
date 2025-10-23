using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Entities;
using DenounceBeasts.API.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MunicipalitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ApiResponse<IEnumerable<Municipality>> GetAll()
        {
            var municipalities = _context.Municipalities.ToList();
            return ApiResponse<IEnumerable<Municipality>>.Success(municipalities);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Municipality> GetById(int id)
        {
            var municipality = _context.Municipalities.FirstOrDefault(s => s.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }
            return Ok(municipality);
        }

        [HttpPost]
        public ActionResult<Municipality> Create(Municipality municipality)
        {
            _context.Municipalities.Add(municipality);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = municipality.Id }, municipality);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Municipality updatedMunicipality)
        {
            var municipality = _context.Municipalities.FirstOrDefault(s => s.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }
            municipality.PostalCode = updatedMunicipality.PostalCode;
            municipality.Name = updatedMunicipality.Name;
            // municipality.IsActive = updatedMunicipality.IsActive;
            _context.Municipalities.Update(municipality);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var municipality = _context.Municipalities.FirstOrDefault(s => s.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }
            _context.Municipalities.Remove(municipality);
            return NoContent();
        }

    }
}
