using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Responses;
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

            var page = await query.ToPageAsync(request);
            return Ok(ApiResponse<PageResult<T>>.SuccessResponse(page));
        }

        [HttpGet]
        public virtual async Task<ActionResult<ApiResponse<List<T>>>> GetAll()
        {
            IQueryable<T> query = Set.AsNoTracking();
            var response = Set.AsNoTracking().ToList();
            return Ok(ApiResponse<List<T>>.SuccessResponse(response));
        }

        [HttpGet("{id:int}")]
        public virtual async Task<ActionResult<ApiResponse<T>>> GetById(int id)
        {
            var entity = await Set.FindAsync(new object[] { id });
            return entity is null ? NotFound(ApiResponse<T>.FailResponse(404, "Not found"))
                : Ok(ApiResponse<T>.SuccessResponse(entity));
        }

        [HttpPost]
        public virtual async Task<ActionResult<ApiResponse<T>>> Create([FromBody] T input)
        {
            Set.Add(input);
            await Context.SaveChangesAsync();
            return Ok(input);
        }

        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var current = await Set.FindAsync(new object[] { id });
            if (current is null) return NotFound(ApiResponse<T>.FailResponse(404, "Not found"));

            Set.Remove(current);
            await Context.SaveChangesAsync();
            return NoContent();
        }


    }


}



