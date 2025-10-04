using Microsoft.EntityFrameworkCore;
using DenounceBeasts.API.Models.Entities;
namespace DenounceBeasts.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Sector> Sectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Municipality>()
                .HasMany(m => m.Sectors)
                .WithOne(s => s.Municipality)
                .HasForeignKey(s => s.MunicipalityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
