using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : BaseController<Status>
    {
        public StatusController(ApplicationDbContext context) : base(context)
        {
        }

    }

    //[ApiController]
    //[Route("api/[controller]")]
    //public class StatusController : ControllerBase
    //{
    //    private readonly ApplicationDbContext _context;

    //    public StatusController(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    [HttpGet]
    //    [Route("GetAll1")]
    //    public ActionResult<List<Status>> GetAll1()
    //    {
    //        var status = _context.Status.AsNoTracking()
    //            .ToList();
    //        return Ok(status);
    //    }

    //    [HttpGet]
    //    [Route("GetAll2")]
    //    public ActionResult<ApiResponse<Status>> GetAll2()
    //    {
    //        var status = _context.Status.AsNoTracking()
    //            .ToList();

    //        if (status == null || status.Count == 0)
    //        {
    //            return NotFound(ApiResponse<List<Status>>.Fail("No status found."));
    //        }
    //        //foreach (var statusItem in status)
    //        //{
    //        //    // Avoid circular reference issues by nullifying the Complaints navigation property
    //        //    statusItem.Name = string.Empty;
    //        //    _context.Entry(statusItem).State = EntityState.Modified;
    //        //    _context.Status.Update(statusItem);
    //        //    _context.Status.Add(statusItem);
    //        //}

    //        //_context.SaveChanges();
    //        return Ok(ApiResponse<List<Status>>.Success(status));

    //    }


    //    [HttpGet]
    //    [Route("GetAll3")]
    //    public async Task<ActionResult<ApiResponse<Status>>> GetAll3Async([FromQuery] PageRequest request)
    //    {
    //        var statusList = await Paging.ToPageAsync(_context.Status.AsNoTracking(), request);

    //        return Ok(ApiResponse<PageResult<Status>>.Success(statusList));
    //    }

    //    [HttpGet]
    //    [Route("{id}")]
    //    public ActionResult<Status> GetById(int id)
    //    {
    //        var status = _context.Status.AsNoTracking().FirstOrDefault(s => s.Id == id);
    //        if (status == null)
    //        {
    //            return NotFound();
    //        }
    //        return Ok(status);
    //    }

    //    [HttpPost]
    //    public ActionResult<Status> Create(Status status)
    //    {
    //        _context.Status.Add(status);
    //        _context.SaveChanges();

    //        return CreatedAtAction(nameof(GetById), new { id = status.Id }, status);
    //    }

    //    [HttpPut("{id}")]
    //    public ActionResult Update(int id, Status updatedStatus)
    //    {
    //        var status = _context.Status.FirstOrDefault(s => s.Id == id);
    //        if (status == null)
    //        {
    //            return NotFound();
    //        }

    //        status.Name = updatedStatus.Name;
    //        _context.Status.Update(status);
    //        _context.SaveChanges();

    //        return NoContent();
    //    }
    //    [HttpDelete("{id}")]
    //    public ActionResult Delete(int id)
    //    {
    //        var status = _context.Status.FirstOrDefault(s => s.Id == id);
    //        if (status == null)
    //        {
    //            return NotFound();
    //        }
    //        _context.Status.Remove(status);
    //        return NoContent();
    //    }

    //}
}
