using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Data;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public MunicipalityRepository MunicipalityRepository { get; }
        public StatusRepository StatusRepository { get; }
        public SectorRepository SectorRepository { get; }
        public GenericRespository<ComplaintType> ComplaintTypeRepository { get; }
        //public ComplaintTypeRepository ComplaintTypeRepository { get; }

        public UnitOfWork(ApplicationDbContext context,
            StatusRepository statusRepository,
            SectorRepository sectorRepository,
            //ComplaintTypeRepository complaintTypeRepository,
            GenericRespository<ComplaintType> complaintTypeRepository,
            MunicipalityRepository municipalityRepository)
        {
            _context = context;
            StatusRepository = statusRepository;
            ComplaintTypeRepository = complaintTypeRepository;
            MunicipalityRepository = municipalityRepository;
            SectorRepository = sectorRepository;
            //Municipalities = new MunicipalityRepository(_context);
            //Sectors = new SectorRepository(_context);
            //Statuses = new StatusRepository(_context);
            //ComplaintTypes = new ComplaintTypeRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }
        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }
    }
}
