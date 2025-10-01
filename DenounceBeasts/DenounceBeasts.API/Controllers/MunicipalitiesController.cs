using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipalitiesController : ControllerBase
    {
        List<Municipality> municipalities = new List<Municipality>()
        {
            new Municipality() { Id = 1, PostalCode = "12345", Name = "Municipality A" },
            new Municipality() { Id = 2, PostalCode = "67890", Name = "Municipality B" },
            new Municipality() { Id = 3, PostalCode = "54321", Name = "Municipality C" }
        };

        [HttpGet]
        public ActionResult<List<Municipality>> GetAll()
        {
            return Ok(municipalities);
        }
         
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Municipality> GetById(int id)
        {
            var municipality = municipalities.FirstOrDefault(s => s.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }
            return Ok(municipality);
        }

        [HttpPost]
        public ActionResult<Municipality> Create(Municipality municipality)
        {
            municipality.Id = municipalities.Count() + 1;
            municipalities.Add(municipality);
            return Ok(municipality);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Municipality> Update(int id, Municipality updatedMunicipality)
        {
            var municipality = municipalities.FirstOrDefault(s => s.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }
            municipality.PostalCode = updatedMunicipality.PostalCode;
            municipality.Name = updatedMunicipality.Name;
            return Ok(municipalities);
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var municipality = municipalities.FirstOrDefault(s => s.Id == id);
            if (municipality == null)
            {
                return NotFound();
            }
            municipalities.Remove(municipality);
            return Ok(municipalities);
        }


    }
}
