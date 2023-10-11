using GameZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>().HasKey(e => new { e.DeviceID, e.GameID });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
          .HasData(new Category[]
          {
                new Category { ID = 1, Name = "Sports" },
                new Category { ID = 2, Name = "Action" },
                new Category { ID = 3, Name = "Adventure" },
                new Category { ID = 4, Name = "Racing" },
                new Category { ID = 5, Name = "Fighting" },
                new Category { ID = 6, Name = "Film" }
          });

            modelBuilder.Entity<Device>()
                .HasData(new Device[]
                {
                new Device { ID = 1, Name = "PlayStation", Icon = "bi bi-playstation" },
                new Device { ID = 2, Name = "xbox", Icon = "bi bi-xbox" },
                new Device { ID = 3, Name = "Nintendo Switch", Icon = "bi bi-nintendo-switch" },
                new Device { ID = 4, Name = "PC", Icon = "bi bi-pc-display" }
                });

        }

        
    }
}
