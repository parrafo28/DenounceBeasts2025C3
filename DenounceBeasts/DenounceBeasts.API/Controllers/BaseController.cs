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
        public ActionResult<ApiResponse<T>> GetAll()
        {
            var entities = Set.ToList();
            return Ok(ApiResponse<List<T>>.Success(entities));
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<T>> GetById(int id)
        {
            var entity = Set.Find(id);
            if (entity == null)
            {
                return NotFound(ApiResponse<T>.Fail("404", "Entity Not Fount"));
            }

            return Ok(ApiResponse<T>.Success(entity));
        }

        [HttpPost]
        public ActionResult<ApiResponse<T>> Create(T request)
        {
            Set.Add(request);
            Context.SaveChanges();
            return Ok(ApiResponse<T>.Success(request));
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<T>> Delete(int id)
        {
            //var entity = Context.Set<T>().Find(id);
            var entity = Set.Find(id);

            if (entity == null)
            {
                return NotFound(ApiResponse<T>.Fail("404", "Entity Not Fount"));
            }
            //Context.Set<T>().Remove(entity);
            Set.Remove(entity);
            Context.SaveChanges();
            return Ok(ApiResponse<T>.Success(entity));
        }
    }
}
