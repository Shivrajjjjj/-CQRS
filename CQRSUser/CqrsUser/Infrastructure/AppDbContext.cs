using CqrsUser.Domain;
using Microsoft.EntityFrameworkCore;

namespace CqrsUser.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserEvent> UserEvents => Set<UserEvent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.UserNumber)
                    .ValueGeneratedOnAdd(); // Auto-increment
                b.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
                b.Property(x => x.LastName).HasMaxLength(100).IsRequired();
                b.Property(x => x.Email).HasMaxLength(200).IsRequired();
                b.Property(x => x.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                b.HasIndex(x => x.Email).IsUnique();
            });

            modelBuilder.Entity<UserEvent>(b =>
            {
                b.HasKey(x => x.EventId);
                b.Property(x => x.EventType).HasMaxLength(100).IsRequired();
                b.Property(x => x.EventData).IsRequired();
                b.Property(x => x.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                b.HasOne<User>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
