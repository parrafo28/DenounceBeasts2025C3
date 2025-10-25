using AutoMapper;
using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.API.Controllers
{
    public class BaseController<T, TDto> : ControllerBase
        where T : class
        where TDto : class
    {
        protected readonly ApplicationDbContext Context;
        protected readonly IMapper Mapper;
        protected readonly DbSet<T> Set;

        public BaseController(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
            Set = context.Set<T>();
        }

        [HttpGet]
        public ActionResult<ApiResponse<TDto>> GetAll()
        {
            var entities = Set.ToList();
            //var response = entities.Select(e => Mapper.Map<TDto>(e)).ToList();
            var response = Mapper.Map<List<TDto>>(entities);
            return Ok(ApiResponse<List<TDto>>.Success(response));
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<TDto>> GetById(int id)
        {
            var entity = Set.Find(id);
            if (entity == null)
            {
                return NotFound(ApiResponse<TDto>.Fail(404, "Entity Not Fount"));
            }
            var response = Mapper.Map<TDto>(entity);
            return Ok(ApiResponse<TDto>.Success(response));
        }

        [HttpPost]
        public ActionResult<ApiResponse<TDto>> Create(TDto request)
        {
            var entity = Mapper.Map<T>(request);
            Set.Add(entity);
            Context.SaveChanges();
            var response = Mapper.Map<TDto>(entity);
            return Ok(ApiResponse<TDto>.Success(response));
        }


        //[HttpPut]
        //public ActionResult<ApiResponse<TDto>> Update(TDto request)
        //{
        //   var entity = Mapper.Map<T>(request);
        //    Set.Update(entity);
        //    Context.SaveChanges(); 
        //    return Ok(ApiResponse<TDto>.Success(null));
        //}

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<TDto>> Delete(int id)
        {
            //var entity = Context.Set<T>().Find(id);
            var entity = Set.Find(id);

            if (entity == null)
            {
                return NotFound(ApiResponse<TDto>.Fail(404, "Entity Not Fount"));
            }
            //Context.Set<T>().Remove(entity);
            Set.Remove(entity);
            Context.SaveChanges();
            return Ok(ApiResponse<TDto>.Success(null));
        }
    }
}
