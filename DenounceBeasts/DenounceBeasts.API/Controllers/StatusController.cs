
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : BaseController<Status>
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Status> Update(int id, Status updatedStatus)
        {
            var status = _context.Status.FirstOrDefault(s => s.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            status.Name = updatedStatus.Name;
            _context.Status.Update(status);
            _context.SaveChanges();
            return Ok(status);
        }
         
    }
}
