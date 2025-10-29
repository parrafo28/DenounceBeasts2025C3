using DenounceBeasts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class MunicipalityRepository
    {
        private readonly ApplicationDbContext _db;

        public MunicipalityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Municipality> GetAll()
        {
            return _db.Municipalities
                  .AsNoTracking().ToList();
        }

        public List<Municipality> GetAllWithSectors()
        {
            return _db.Municipalities.Include(p => p.Sectors)
                  .AsNoTracking().ToList();
        }

        public Municipality GetById(int id)
        {
            return _db.Municipalities.FirstOrDefault(s => s.Id == id);
        }

        public int Create(Municipality request)
        {
            _db.Municipalities.Add(request);
            return request.Id;
        }

        public void Update(int id, Municipality updatedMunicipality)
        {
            var municipality = GetById(id);

            if (municipality == null)
            {
                throw new Exception("Municipality Not found");
            }

            municipality.PostalCode = updatedMunicipality.PostalCode;
            municipality.Name = updatedMunicipality.Name;
            _db.Municipalities.Update(municipality);


        }

        public void Delete(int id)
        {
            var municipality = GetById(id);
            if (municipality == null)
            {
                throw new Exception("Municipality Not found");
            }

            _db.Municipalities.Remove(municipality);
        }


    }
}
