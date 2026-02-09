using MemberManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // needed for PasswordHasher

namespace MemberManagement.Infrastructure
{
    public class MMSDbContext : DbContext
    {
        public MMSDbContext(DbContextOptions<MMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Member configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.BirthDate)
                      .HasColumnType("date");
            });

            // AppUser configuration
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.IsActive)
                      .HasDefaultValue(true);

                entity.Property(e => e.DateCreated)
                      .HasDefaultValueSql("GETDATE()");
            });

            // Seed default admin user
            var hasher = new PasswordHasher<AppUser>();
            var defaultUser = new AppUser
            {
                UserID = 1,
                Username = "admin",
                PasswordHash = hasher.HashPassword(null, "Admin123!"),
                IsActive = true,
                DateCreated = DateTime.Now
            };

            modelBuilder.Entity<AppUser>().HasData(defaultUser);

            base.OnModelCreating(modelBuilder);
        }
    }
}
