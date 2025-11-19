using DenounceBeasts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class SectorRepository
    {
        private readonly ApplicationDbContext _db;

        public SectorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Sector> GetAll()
        {
            return _db.Sectors
                  .AsNoTracking().ToList();
        }

        public List<Sector> GetAllWithMunicipality()
        {
            return _db.Sectors.Include(p => p.Municipality)
                  .AsNoTracking().ToList();
        }

        public Sector GetById(int id)
        {
            return _db.Sectors.FirstOrDefault(s => s.Id == id);
        }

        public List<Sector> GetSectorsByMunicipality(int id)
        {
            return _db.Sectors.Where((s => s.MunicipalityId == id)).AsNoTracking()
                  .ToList();
        }

        public int Create(Sector request)
        {
            _db.Sectors.Add(request);
            return request.Id;
        }

        public void Update(int id, Sector updatedSector)
        {
            var sector = GetById(id);

            if (sector == null)
            {
                throw new Exception("Sector Not found");
            }

            sector.PostalCode = updatedSector.PostalCode;
            sector.Name = updatedSector.Name;
            _db.Sectors.Update(sector);


        }

        public void Delete(int id)
        {
            var sector = GetById(id);
            if (sector == null)
            {
                throw new Exception("Sector Not found");
            }

            _db.Sectors.Remove(sector);
        }


    }
}
