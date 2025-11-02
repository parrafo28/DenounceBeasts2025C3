using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Context;

namespace DenounceBeasts.Infrasctructure.Repositories
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public SectorRepository SectorRepository { get; }
        public MunicipalityRepository MunicipalityRepository { get; }
        public StatusRepository StatusRepository { get; }
        //public ComplaintTypeRepository ComplaintTypeRepository { get; }
        public GenericRepository<ComplaintType> ComplaintTypeRepository { get; }

        public UnitOfWork(ApplicationDbContext context, SectorRepository sectorRepository,
          MunicipalityRepository municipalityRepository,
          //ComplaintTypeRepository complaintTypeRepository, 
          GenericRepository<ComplaintType> complaintTypeRepository,
          StatusRepository statusRepository)
        {
            _context = context;
            SectorRepository = sectorRepository;
            MunicipalityRepository = municipalityRepository;
            ComplaintTypeRepository = complaintTypeRepository;
            StatusRepository = statusRepository;
        }

        //public SectorRepository SectorRepository => _sectorRepository ?? new SectorRepository(_context);
        //public SectorRepository SectorRepository => (_sectorRepository == null) ? new SectorRepository(_context): _sectorRepository;

        // public SectorRepository SectorRepository => new SectorRepository(_context);

        //public SectorRepository SectorRepository
        //{
        //    get
        //    { 
        //        return new SectorRepository(_context);
        //    }
        //}

        //public SectorRepository SectorRepository
        //{
        //    get
        //    { 
        //        _sectorRepository = new SectorRepository(_context); 
        //        return _sectorRepository;
        //    }
        //}

        //public SectorRepository SectorRepository
        //{
        //    get
        //    {
        //        if (_sectorRepository == null)
        //        {
        //            _sectorRepository = new SectorRepository(_context);
        //        }
        //        return _sectorRepository;
        //    }
        //}
        //public MunicipalityRepository MunicipalityRepository
        //{
        //    get
        //    {
        //        if (_municipalityRepository == null)
        //        {
        //            _municipalityRepository = new MunicipalityRepository(_context);
        //        }
        //        return _municipalityRepository;
        //    }
        //}
        //public StatusRepository StatusRepository
        //{
        //    get
        //    {
        //        if (_statusRepository == null)
        //        {
        //            _statusRepository = new StatusRepository(_context);
        //        }
        //        return _statusRepository;
        //    }
        //}

        public void Complete()
        {
            _context.SaveChanges();
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

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
