using Microsoft.EntityFrameworkCore;
using TelephoneCRUD.Models;

namespace TelephoneCRUD.Data
{
    public class TelephoneContext : DbContext
    {
        public DbSet<Telephone> Telephones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Utilise LocalDB pour le développement
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TelephoneDB;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Telephone>(entity =>
            {
                entity.HasKey(e => e.Imei);
                entity.Property(e => e.Imei).HasMaxLength(15);
                entity.Property(e => e.Marque).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Modele).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Prix).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.DateAjout).HasDefaultValueSql("GETDATE()");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}