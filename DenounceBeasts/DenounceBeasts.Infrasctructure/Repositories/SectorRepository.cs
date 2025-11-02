using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class SectorRepository: GenericRepository<Sector>
    {
        readonly ApplicationDbContext _context;

        public SectorRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        //public List<Sector> GetAll()
        //{
        //    return _context.Sectors
        //            .ToList();
        //}
        public List<Sector> GetAllWithMunicipality()
        {
            return _context.Sectors.Include(p => p.Municipality)
                    .ToList();
        }

        //public Sector GetById(int id) => _context.Sectors.FirstOrDefault(s => s.Id == id);


        public Sector GetByIdWithMunicipality(int id)
        {
            return _context.Sectors.Include(p => p.Municipality)
                .FirstOrDefault(s => s.Id == id);
        }

        //public Sector GetByIdAndValidate(int id)
        //{
        //    var sector = GetById(id);
        //    if (sector == null)
        //    {
        //        throw new KeyNotFoundException("Sector not found");
        //    }
        //    return sector;
        //}

        //public int Create(Sector request)
        //{

        //    _context.Sectors.Add(request);
        //    return request.Id;
        //}

        //public Sector CreateAndReturnEntity(Sector request)
        //{

        //    _context.Sectors.Add(request);
        //    return request;
        //}

        public void Update(int id, Sector updatedSector)
        {
            var sector = GetById(id);

            if (sector == null)
            {
                throw new KeyNotFoundException("Sector not found");
            }
            sector.Name = updatedSector.Name;
            sector.PostalCode = updatedSector.PostalCode;
            sector.MunicipalityId = updatedSector.MunicipalityId;
            _context.Sectors.Update(sector);
        }

        //public void Update(Sector updatedSector)
        //{
        //    _context.Sectors.Update(updatedSector);
        //}

        //public void Delete(int id)
        //{
        //    var sector = GetByIdAndValidate(id);
        //    _context.Sectors.Remove(sector);
        //}
        //public void SaveChanges()
        //{
        //    _context.SaveChanges();

        //}
    }
}
