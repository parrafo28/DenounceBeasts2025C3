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
            new Sector { Id = 1, PostalCode = "12345", Name = "Sector A", IsActive = true },
            new Sector { Id = 2, PostalCode = "67890", Name = "Sector B", IsActive = false },
            new Sector { Id = 3, PostalCode = "54321", Name = "Sector C", IsActive = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Sector>> GetAll()
        {
            return Ok(sectors);
        }
         
        [HttpGet]
        [Route("{id}")]
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
        public ActionResult<Sector> Create(Sector sector)
        {
            sector.Id = sectors.Count() + 1;
            sectors.Add(sector);
            return Ok(sectors);

            //return CreatedAtAction(nameof(GetById), new { id = sector.Id }, sector);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Sector updatedSector)
        {
            var sector = sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.PostalCode = updatedSector.PostalCode;
            sector.Name = updatedSector.Name;
            sector.IsActive = updatedSector.IsActive;
            return Ok(sectors);

            // return NoContent();
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
            return Ok(sectors);
            // return NoContent();
        }

    }
}
