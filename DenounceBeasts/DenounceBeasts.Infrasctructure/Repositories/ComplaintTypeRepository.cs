using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class ComplaintTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ComplaintType> GetAll()
        {
            var complaintTypes = _context.ComplaintTypes
                .ToList();
            return complaintTypes;
        }
         

        public ComplaintType GetById(int id)
        {
            var complaintType = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);
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

        public void Update(int id, ComplaintType updatedComplaintType)
        {
            var complaintType = GetById(id);
            if (complaintType == null)
            {
                throw new KeyNotFoundException("ComplaintType not found");
            }
            complaintType.Description = updatedComplaintType.Description;
            complaintType.Name = updatedComplaintType.Name;
            _context.ComplaintTypes.Update(complaintType);
        }

        public void Delete(int id)
        {
            var complaintType = GetById(id);
            if (complaintType == null)
            {
                throw new KeyNotFoundException("ComplaintType not found");
            }
            _context.ComplaintTypes.Remove(complaintType);
        }

       
    }
}
