using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class SectorRepository: GenericRespository<Sector>
    {
        private readonly ApplicationDbContext _context;

        public SectorRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        //public List<Sector> GetAll()
        //{
        //    var sectors = _context.Sectors
        //        .ToList();
        //    return sectors;
        //}

        public List<Sector> GetAllWithMuniciplity()
        {
            var sectors = _context.Sectors
                .Include(p => p.Municipality)
                .ToList();
            return sectors;
        }

        //public Sector GetById(int id)
        //{
        //    var sector = _context.Sectors.FirstOrDefault(s => s.Id == id);
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

        //public void Update(//int id, 
        //    Sector updatedSector)
        //{
        //    // var sector = GetById(id);

        //    //sector.PostalCode = updatedSector.PostalCode;
        //    //sector.Name = updatedSector.Name;
        //    //_context.Sectors.Update(sector);

        //    updatedSector.PostalCode = updatedSector.PostalCode;
        //    updatedSector.Name = updatedSector.Name;
        //    _context.Sectors.Update(updatedSector);
             
        //}

        //public void Delete(int id)
        //{
        //    var sector = GetById(id);
        
        //    _context.Sectors.Remove(sector);
       
        //}
        //public void SaveChanges()
        //{
        //    _context.SaveChanges();
        //}
    }
}
