using DenounceBeasts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<ComplaintType> ComplaintTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                entity.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //Bill ==> BillDetails <== Products

            //Client


            //fluent api
            //modelBuilder.Entity<Municipality>()
            //    .HasMany(m => m.Sectors)
            //    .WithOne(s => s.Municipality)
            //    .HasForeignKey(s => s.MunicipalityId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComplaintType>(ct =>
            {
                ct.HasKey(x => x.Id);
                ct.Property(x => x.Name).IsRequired().HasMaxLength(150);
            });

            //modelBuilder.Entity<Vote>()
            // .HasIndex(v => new { v.UserId, v.ComplaintId })
            // .IsUnique();
        }
    }
}
