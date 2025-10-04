using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        List<Sector> sectors = new List<Sector>
        {
            new Sector { Id = 1, IsActive = true, PostalCode = "12345", Name = "Sector A" },
            new Sector { Id = 2, IsActive = true, PostalCode = "67890", Name = "Sector B" },
            new Sector { Id = 3, IsActive = false, PostalCode = "54321", Name = "Sector C" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Sector>> GetAll()
        {
            return Ok(sectors);
        }

        [HttpGet]
        [Route("GetAllSector")]
        public ActionResult<IEnumerable<Sector>> GetAllx()
        {
            return Ok(sectors);
        }

        [HttpGet("GetAllSectorv2")]
        public ActionResult<IEnumerable<Sector>> GetAllxx()
        {
            return Ok(sectors);
        }

        [HttpGet("/GetAllSectorv3")]
        public ActionResult<IEnumerable<Sector>> GetAllxxx()
        {
            return Ok(sectors);
        }
        // [HttpGet("{id:int}")]
        [HttpGet("{id}")]
        public ActionResult<Sector> GetById(int id)
        {
            var sector = sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            return Ok(sector);
        }

        [HttpPost]
        public ActionResult<List<Sector>> Create(Sector sector)
        {
            sector.Id = sectors.Count() + 1;
            sectors.Add(sector);
            //return CreatedAtAction(nameof(GetById), new { id = sector.Id }, sector);
            return Ok(sectors);
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, Sector updatedSector)
        {
            var sector = sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.Name = updatedSector.Name;
            sector.PostalCode = updatedSector.PostalCode;
            sector.IsActive = updatedSector.IsActive;
            //return NoContent();
            return Ok(sectors);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var sector = sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sectors.Remove(sector);
            //return NoContent();
            return Ok(sectors);
        }

    }
}
