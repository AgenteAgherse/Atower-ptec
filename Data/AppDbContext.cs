using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ZipFileRecord> ZipFileRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZipFileRecord>()
                .ToTable("zips")
            .HasKey(z => z.Id);

            modelBuilder.Entity<ZipFileRecord>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ZipFileRecord>()
                .Property(z => z.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
