using DenounceBeasts.Business.Responses;
using DenounceBeasts.Infrasctructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.API.Controllers
{
    public class BaseController<T> : ControllerBase where T : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<T> Set;

        public BaseController(ApplicationDbContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        [HttpGet]
        [Route("GetAllPaginated")]
        public virtual async Task<ActionResult<ApiResponse<PageResult<T>>>> GetAllPaginated([FromQuery] PageRequest request)
        {
            IQueryable<T> query = Set.AsNoTracking();

            var paged = await query.ToPageAsync(request);
            return Ok(ApiResponse<PageResult<T>>.Success(paged));
        }

        [HttpGet]
        public virtual async Task<ActionResult<T>> GetAll()
        {
            IQueryable<T> query = Set.AsNoTracking();
            var response = Set.AsNoTracking().ToList();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public virtual async Task<ActionResult<ApiResponse<T>>> GetById(int id)
        {
            var entity = await Set.FindAsync(new object[] { id });
            return (entity is null) ? NotFound(ApiResponse<T>.Fail(404, "Not found"))
                : Ok(ApiResponse<T>.Success(entity));
        }

        [HttpPost]
        public virtual async Task<ActionResult<ApiResponse<T>>> Create([FromBody] T input)
        {
            Set.Add(input);
            await Context.SaveChangesAsync();

            // return CreatedAtAction(nameof(GetById), new { id = input.Id }, ApiResponse<T>.Success(input));
            //return CreatedAtAction(nameof(GetById), new { input }, ApiResponse<T>.Success(input));
            return Ok(input);
        }

        //[HttpPut("{id:int}")]
        //public virtual async Task<IActionResult> Update(int id, [FromBody] T input )
        //{
        //    var current = await Set.FindAsync(new object[] { id });
        //    if (current is null) return NotFound(ApiResponse<T>.Fail("Not found", "404"));

        //    // Mapeo manual controlado (evitamos AutoMapper por ahora)
        //    current = CopyUpdatableFieldsFrom(input);
        //    current.Updated = DateTime.UtcNow;

        //    await Db.SaveChangesAsync(ct);
        //    return NoContent();
        //}

        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var current = await Set.FindAsync(new object[] { id });
            if (current is null) return NotFound(ApiResponse<T>.Fail(404, "Not found"));

            Set.Remove(current);
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
