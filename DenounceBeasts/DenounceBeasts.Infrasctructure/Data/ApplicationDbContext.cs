using DenounceBeasts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Infrasctructure.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<ComplaintType> ComplaintTypes { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the one-to-many relationship between Municipality and Sector
            //modelBuilder.Entity<Municipality>()
            //    .HasMany(m => m.Sectors)
            //    .WithOne(s => s.Municipality)
            //    .HasForeignKey(s => s.MunicipalityId)
            //    .OnDelete(DeleteBehavior.Cascade); // Optional: specify delete behavior
            // Additional configurations can be added here if needed

            modelBuilder.Entity<ComplaintType>(p =>
            {
                p.HasKey(c => c.Id);
                p.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            //modelBuilder.Entity<Vote>()
            // .HasIndex(v => new { v.UserId, v.ComplaintId })
            // .IsUnique();
        }
    }
}
