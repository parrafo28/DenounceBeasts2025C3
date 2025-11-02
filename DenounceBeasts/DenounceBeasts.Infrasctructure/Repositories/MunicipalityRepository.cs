using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class MunicipalityRepository
    {
        readonly ApplicationDbContext _context;

        public MunicipalityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Municipality> GetAll()
        {
            return _context.Municipalities
                    .ToList();
        }
        public List<Municipality> GetAllWithSectors()
        {
            return _context.Municipalities.Include(p => p.Sectors)
                    .ToList();
        }

        public Municipality GetById(int id) => _context.Municipalities.FirstOrDefault(s => s.Id == id);


        public Municipality GetByIdWithSectors(int id)
        {
            return _context.Municipalities.Include(p => p.Sectors)
                .FirstOrDefault(s => s.Id == id);
        }

        public Municipality GetByIdAndValidate(int id)
        {
            var municipality = GetById(id);
            if (municipality == null)
            {
                throw new KeyNotFoundException("Municipality not found");
            }
            return municipality;
        }

        public int Create(Municipality request)
        {

            _context.Municipalities.Add(request);
            return request.Id;
        }

        public Municipality CreateAndReturnEntity(Municipality request)
        {

            _context.Municipalities.Add(request);
            return request;
        }

        public void Update(int id, Municipality updatedMunicipality)
        {
            var municipality = GetById(id);

            if (municipality == null)
            {
                throw new KeyNotFoundException("Municipality not found");
            }
            municipality.Name = updatedMunicipality.Name;
            municipality.PostalCode = updatedMunicipality.PostalCode; 
            _context.Municipalities.Update(municipality);
        }

        public void Update(Municipality updatedMunicipality)
        {
            _context.Municipalities.Update(updatedMunicipality);
        }

        public void Delete(int id)
        {
            var municipality = GetByIdAndValidate(id);
            _context.Municipalities.Remove(municipality);
        }
      
    }
}
