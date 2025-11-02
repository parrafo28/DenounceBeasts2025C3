using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class ComplaintTypeRepository
    {
        readonly ApplicationDbContext _context;

        public ComplaintTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ComplaintType> GetAll()
        {
            return _context.ComplaintTypes
                    .ToList();
        }
 

        public ComplaintType GetById(int id) => _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);


        
        public ComplaintType GetByIdAndValidate(int id)
        {
            var complaintType = GetById(id);
            if (complaintType == null)
            {
                throw new KeyNotFoundException("ComplaintType not found");
            }
            return complaintType;
        }

        public int Create(ComplaintType request)
        { 
            _context.ComplaintTypes.Add(request);
            return request.Id;
        }

        public ComplaintType CreateAndReturnEntity(ComplaintType request)
        { 
            _context.ComplaintTypes.Add(request);
            return request;
        }

        public void Update(int id, ComplaintType updatedComplaintType)
        {
            var complaintType = GetById(id);

            if (complaintType == null)
            {
                throw new KeyNotFoundException("ComplaintType not found");
            }
            complaintType.Name = updatedComplaintType.Name; 
            complaintType.Description = updatedComplaintType.Description;
            _context.ComplaintTypes.Update(complaintType);
        }

        public void Update(ComplaintType updatedComplaintType)
        {
            _context.ComplaintTypes.Update(updatedComplaintType);
        }

        public void Delete(int id)
        {
            var complaintType = GetByIdAndValidate(id);
            _context.ComplaintTypes.Remove(complaintType);
        }

   
    }
}
