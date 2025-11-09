using DenounceBeasts.Domain.Core;
using DenounceBeasts.Infrasctructure.Context;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class GenericRepository<TEntity> where TEntity : BaseEntity
    {
        readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>()
                    .ToList();
        }


        public TEntity GetById(int id) => _context.Set<TEntity>().FirstOrDefault(s => s.Id == id);



        public TEntity GetByIdAndValidate(int id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }
            return entity;
        }

        public void Create(TEntity request)
        {
            _context.Set<TEntity>().Add(request); 
        }

        public TEntity CreateAndReturnEntity(TEntity request)
        {
            _context.Set<TEntity>().Add(request);
            return request;
        }

        public void Update(TEntity updatedComplaintType)
        {
            _context.Set<TEntity>().Update(updatedComplaintType);
        }

        public void Delete(int id)
        {
            var entity = GetByIdAndValidate(id);
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
