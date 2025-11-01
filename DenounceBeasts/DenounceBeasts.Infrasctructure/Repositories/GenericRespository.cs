using DenounceBeasts.Domain.Core;
using DenounceBeasts.Infrasctructure.Data;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class GenericRespository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRespository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TEntity> GetAll()
        {
            var entities = _context.Set<TEntity>()
                .ToList();
            return entities;
        }


        public TEntity GetById(int id)
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(s => s.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }

            return entity;
        }

        public int Create(TEntity request)
        {
            _context.Set<TEntity>().Add(request);

            return request.Id;
        }

        public void Update(TEntity updatedEntity)
        {
            _context.Set<TEntity>().Update(updatedEntity);
        }
         
        public void Delete(int id)
        {
            var entity = GetById(id);

            _context.Set<TEntity>().Remove(entity);

        }
        //public void SaveChanges()
        //{
        //    _context.SaveChanges();
        //}
    }
}
