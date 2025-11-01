using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class MunicipalityRepository
    {
        private readonly ApplicationDbContext _context;

        public MunicipalityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Municipality> GetAll()
        {
            var municipalities = _context.Municipalities
                .ToList();
            return municipalities;
        }

        public List<Municipality> GetAllWithSectors()
        {
            var municipalities = _context.Municipalities
                .Include(p => p.Sectors)
                .ToList();
            return municipalities;
        }

        public Municipality GetById(int id)
        {
            var municipality = _context.Municipalities.FirstOrDefault(s => s.Id == id);
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

        public void Update(int id, Municipality updatedMunicipality)
        {
            var municipality = GetById(id);

            municipality.PostalCode = updatedMunicipality.PostalCode;
            municipality.Name = updatedMunicipality.Name;
            _context.Municipalities.Update(municipality);
        }

        public void Delete(int id)
        {
            var municipality = GetById(id);
          
            _context.Municipalities.Remove(municipality);
        }
       
    }
}
