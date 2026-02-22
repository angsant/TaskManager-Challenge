using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the TaskItem entity explicitly
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id); // Primary Key

                entity.Property(t => t.Titulo)
                    .IsRequired()
                    .HasMaxLength(100); // Enforce strict limit in DB [cite: 21]

                entity.Property(t => t.Status)
                    .HasConversion<string>(); // Save Enum as "Pendente" instead of 0 (Better readability)
            });
        }
    }
}