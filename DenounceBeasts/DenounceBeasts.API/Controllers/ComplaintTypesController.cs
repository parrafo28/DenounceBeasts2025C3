using DenounceBeasts.Application.Dtos;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ComplaintTypesController(ApplicationDbContext context)
        {

            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ComplaintTypeDto>> GetAll()
        {
            var complaintTypes = _context.ComplaintTypes.Skip(15).Take(15).AsNoTracking().ToList();
            var list = new List<ComplaintTypeDto>();

            var selectedComplaintTypes = complaintTypes.Select(s => new ComplaintTypeDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            }).ToList();

            return Ok(selectedComplaintTypes);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ComplaintType> GetById(int id)
        {
            var complaintType = _context.ComplaintTypes.AsNoTracking().FirstOrDefault(s => s.Id == id);
            if (complaintType == null)
            {
                return NotFound();
            }
            return Ok(complaintType);
        }

        [HttpPost]
        public ActionResult<ComplaintTypeDto> Create(ComplaintTypeDto complaintType)
        {

            var complaintTypeAtDb = new ComplaintType
            {
                Description = complaintType.Description,
                Name = complaintType.Name
            };

            _context.ComplaintTypes.Add(complaintTypeAtDb);
            _context.SaveChanges();
            return Ok(complaintTypeAtDb.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ComplaintType> Update(int id, ComplaintType updatedComplaintType)
        {
            var complaintType = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);

            if (complaintType != null)
            {
                complaintType.Description = updatedComplaintType.Description;
                complaintType.Name = updatedComplaintType.Name;
                _context.ComplaintTypes.Update(complaintType);
                _context.SaveChanges();
                return NoContent();
            }

            return NotFound();

        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var complaintType = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);
            if (complaintType == null)
            {
                return NotFound();
            }
            _context.ComplaintTypes.Remove(complaintType);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
