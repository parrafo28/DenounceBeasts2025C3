using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintTypesController : BaseController<ComplaintType>
    {
        public ComplaintTypesController(ApplicationDbContext context) : base(context)
        {

        }

        //private readonly ApplicationDbContext _context;

        //public ComplaintTypesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<ComplaintType>> GetAll()
        //{
        //    var complaintTypes = _context.ComplaintTypes.ToList();
        //    return Ok(complaintTypes);
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public ActionResult<ComplaintType> GetById(int id)
        //{
        //    var complaintType = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);
        //    if (complaintType == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(complaintType);
        //}

        //[HttpPost]
        //public ActionResult<ComplaintType> Create(ComplaintType complaintType)
        //{
        //    _context.ComplaintTypes.Add(complaintType);
        //    _context.SaveChanges();

        //    return CreatedAtAction(nameof(GetById), new { id = complaintType.Id }, complaintType);
        //}

        [HttpPut("{id}")]
        public ActionResult Update(int id, ComplaintType updatedComplaintType)
        {
            var complaintType = Context.ComplaintTypes.FirstOrDefault(s => s.Id == id);
            if (complaintType == null)
            {
                return NotFound();
            }
            complaintType.Description = updatedComplaintType.Description;
            complaintType.Name = updatedComplaintType.Name;
            Context.ComplaintTypes.Update(complaintType);
            Context.SaveChanges();

            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    var complaintType = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);
        //    if (complaintType == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.ComplaintTypes.Remove(complaintType);
        //    return NoContent();
        //}

    }
}
