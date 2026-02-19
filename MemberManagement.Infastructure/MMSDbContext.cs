using MemberManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemberManagement.Infrastructure
{
    public class MMSDbContext : DbContext
    {
        public MMSDbContext(DbContextOptions<MMSDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Member> Members { get; set; }
        public DbSet<Branch> Branches { get; set; } // Add Branch table
        public DbSet<MembershipType> MembershipTypes { get; set; } // Add Branch table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Member configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.BirthDate)
                      .HasColumnType("date");

                // Configure relationship to Branch
                entity.HasOne(m => m.Branch)
                      .WithMany(b => b.Members)
                      .HasForeignKey(m => m.BranchID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure IsActive default value
                entity.Property(m => m.IsActive)
                      .HasDefaultValue(true);
            });

            // Branch configuration
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(b => b.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(b => b.Location)
                      .HasMaxLength(200);

                // Configure IsActive default value
                entity.Property(b => b.IsActive)
                      .HasDefaultValue(true);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
