using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class StatusRepository: GenericRespository<Status>
    {
       // private readonly ApplicationDbContext _context;

        public StatusRepository(ApplicationDbContext context): base(context)
        {
           // _context = context;
        }

        //public List<Status> GetAll()
        //{
        //    var status = _context.Status
        //        .ToList();
        //    return status;
        //}
  
        //public Status GetById(int id)
        //{
        //    var status = _context.Status.FirstOrDefault(s => s.Id == id);
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

        //public void Update(int id, Status updatedStatus)
        //{
        //    var status = GetById(id);
            
        //    status.Name = updatedStatus.Name;
        //    _context.Status.Update(status);
      
        //}

        //public void Delete(int id)
        //{
        //    var status = GetById(id);
            
        //    _context.Status.Remove(status);
      
        //}
       
    }
}
