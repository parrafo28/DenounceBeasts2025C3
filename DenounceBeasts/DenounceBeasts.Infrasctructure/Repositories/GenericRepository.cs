using DenounceBeasts.Domain.Core;
using DenounceBeasts.Domain.Entities; 
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class GenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<T> GetAll()
        {
            return _db.Set<T>()
                  .AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            return _db.Set<T>().Find( id);
        }

        public int Create(T request)
        {
            _db.Set<T>().Add(request);
            return request.Id;
        }

       
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new Exception("Entiry Not found");
            }

            _db.Set<T>().Remove(entity);
        }


    }
}
