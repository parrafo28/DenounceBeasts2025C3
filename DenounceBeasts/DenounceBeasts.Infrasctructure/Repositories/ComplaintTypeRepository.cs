using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class ComplaintTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public ComplaintTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<ComplaintType> GetAll()
        {
            return _db.ComplaintTypes
                  .AsNoTracking().ToList();
        }

        
        public ComplaintType GetById(int id)
        {
            return _db.ComplaintTypes.FirstOrDefault(s => s.Id == id);
        }

        
        public int Create(ComplaintType request)
        {
            _db.ComplaintTypes.Add(request);
            return request.Id;
        }

        public void Update(int id, ComplaintType updatedComplaintType)
        {
            var complaintType = GetById(id);

            if (complaintType == null)
            {
                throw new Exception("ComplaintType Not found");
            }

            complaintType.Description = updatedComplaintType.Description;
            complaintType.Name = updatedComplaintType.Name;
            _db.ComplaintTypes.Update(complaintType);


        }

        public void Delete(int id)
        {
            var complaintType = GetById(id);
            if (complaintType == null)
            {
                throw new Exception("ComplaintType Not found");
            }

            _db.ComplaintTypes.Remove(complaintType);
        }

     
    }
}
