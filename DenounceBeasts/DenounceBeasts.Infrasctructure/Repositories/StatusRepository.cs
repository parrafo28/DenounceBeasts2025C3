using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class StatusRepository : GenericRepository<Status>
    {
        readonly ApplicationDbContext _context;

        public StatusRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public List<Status> GetAll()
        //{
        //    return _context.Status
        //            .ToList();
        //}

        //public Status GetById(int id) => _context.Status.FirstOrDefault(s => s.Id == id);



        //public Status GetByIdAndValidate(int id)
        //{
        //    var status = GetById(id);
        //    if (status == null)
        //    {
        //        throw new KeyNotFoundException("Status not found");
        //    }
        //    return status;
        //}

        //public int Create(Status request)
        //{

        //    _context.Status.Add(request);
        //    return request.Id;
        //}

        //public Status CreateAndReturnEntity(Status request)
        //{

        //    _context.Status.Add(request);
        //    return request;
        //}

        public void Update(int id, Status updatedStatus)
        {
            var status = GetById(id);

            if (status == null)
            {
                throw new KeyNotFoundException("Status not found");
            }
            status.Name = updatedStatus.Name;
            _context.Status.Update(status);
        }

        //public void Update(Status updatedStatus)
        //{
        //    _context.Status.Update(updatedStatus);
        //}

        //public void Delete(int id)
        //{
        //    var status = GetByIdAndValidate(id);
        //    _context.Status.Remove(status);
        //}

    }
}
