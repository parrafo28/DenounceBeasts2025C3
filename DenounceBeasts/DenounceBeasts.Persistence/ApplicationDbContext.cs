using DenounceBeasts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.Persistence
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

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
               .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<ComplaintType>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.Name).HasMaxLength(150).IsRequired();
            });
            //modelBuilder.Entity<Vote>()
            // .HasIndex(v => new { v.UserId, v.ComplaintId })
            // .IsUnique();

            // Complaint - Status relationship  
            //modelBuilder.Entity<Complaint>()
            //    .HasOne(c => c.Status)
            //    .WithMany(s => s.Complaints)
            //    .HasForeignKey(c => c.StatusId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
