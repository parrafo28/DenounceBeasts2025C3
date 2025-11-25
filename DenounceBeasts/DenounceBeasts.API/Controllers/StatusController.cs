using AutoMapper;
using DenounceBeasts.Application.DTOs;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;
using DenounceBeasts.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : BaseController<Status, StatusDto>
    {
        //readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context, Mapper map) : base(context, map)
        {
            //_context = context;
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, SectorDto updatedSector)
        {
            var sector = Context.Sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            sector.Name = updatedSector.Name;
            sector.PostalCode = updatedSector.PostalCode;
            //sector.IsActive = updatedSector.IsActive;
            sector.MunicipalityId = updatedSector.MunicipalityId;
            Context.Sectors.Update(sector);
            Context.SaveChanges();
            return NoContent();
        }
    }
}
