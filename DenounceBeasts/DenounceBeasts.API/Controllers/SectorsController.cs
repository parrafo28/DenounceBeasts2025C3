using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        List<Sector> sectors = new List<Sector>()
        {
            new Sector() { Id = 1, PostalCode = "12345", Name = "Sector A" },
            new Sector() { Id = 2, PostalCode = "67890", Name = "Sector B" },
            new Sector() { Id = 3, PostalCode = "54321", Name = "Sector C" }
        };

        [HttpGet]
        public ActionResult<List<Sector>> GetAll()
        {
            return Ok(sectors);
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
            return Ok(sector);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Sector> Update(int id, Sector updatedSector)
        {
            var sector = sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.PostalCode = updatedSector.PostalCode;
            sector.Name = updatedSector.Name;
            return Ok(sectors);
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var sector = sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sectors.Remove(sector);
            return Ok(sectors);
        }


    }
}
