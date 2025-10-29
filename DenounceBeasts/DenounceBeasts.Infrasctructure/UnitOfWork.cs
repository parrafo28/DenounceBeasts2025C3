using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infrasctructure.Repositories;

namespace DenounceBeasts.Infrasctructure
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public MunicipalityRepository MunicipalityRepository { get; }
        public SectorRepository SectorRepository { get; }
        public StatusRepository StatusRepository { get; }
        //public ComplaintTypeRepository ComplaintTypeRepository { get; }
        public GenericRepository<ComplaintType> ComplaintTypeRepository { get; }

        public UnitOfWork(ApplicationDbContext db,
            GenericRepository<ComplaintType> complaintTypeRepository, StatusRepository statusRepository,
            SectorRepository sectorRepository, MunicipalityRepository municipalityRepository)
        {
            this._db = db;
            ComplaintTypeRepository = complaintTypeRepository;
            StatusRepository = statusRepository;
            SectorRepository = sectorRepository;
            MunicipalityRepository = municipalityRepository;
        }


        public void Complete()
        {
            _db.SaveChanges();
        }

        public void BeginTransaction()
        {
            _db.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _db.Database.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            _db.Database.RollbackTransaction();
        }

    }
}
