using DenounceBeasts.Domain.Entities;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class StatusRepository : GenericRepository<Status>
    {
        private readonly ApplicationDbContext _db;

        public StatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(int id, Status updatedStatus)
        {
            var status = GetById(id);

            if (status == null)
            {
                throw new Exception("Status Not found");
            }

            status.Name = updatedStatus.Name;
            _db.Status.Update(status); 
        }


    }
}
