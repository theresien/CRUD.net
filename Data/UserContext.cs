using Microsoft.EntityFrameworkCore;
using UserCRUD.Models;

namespace UserCRUD.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Utilise LocalDB pour le développement
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UserDB;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Matricule);
                entity.Property(e => e.Matricule).HasMaxLength(20);
                entity.Property(e => e.Nom).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Prenom).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.DateCreation).HasDefaultValueSql("GETDATE()");
                
                // Index unique sur l'email
                entity.HasIndex(e => e.Email).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}