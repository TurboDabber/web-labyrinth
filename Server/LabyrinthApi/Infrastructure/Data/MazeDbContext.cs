using Microsoft.EntityFrameworkCore;
using LabyrinthApi.Domain.Entities;

namespace LabyrinthApi.Infrastructure.Data
{
    public class MazeDbContext : DbContext
    {
        public DbSet<Maze> Mazes { get; set; }
        public MazeDbContext(DbContextOptions<MazeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Maze>()
                .Ignore(m => m.MazeData);

            modelBuilder.Entity<Maze>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Width).IsRequired();
                entity.Property(m => m.Height).IsRequired();
                entity.Property(m => m.MazeDataJson).IsRequired(); 
                entity.Property(m => m.AlgorithmType).IsRequired();
            });
        }
    }
}
