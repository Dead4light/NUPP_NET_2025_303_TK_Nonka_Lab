using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zoo.Infrastructure.Data;

namespace Zoo.Infrastructure
{
    public class ZooContext : IdentityDbContext
    {
        public DbSet<LionModel> Lions { get; set; }
        public DbSet<ParrotModel> Parrots { get; set; }
        public DbSet<ZookeeperModel> Zookeepers { get; set; }

        public ZooContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LionModel>().ToTable("Lions");
            modelBuilder.Entity<ParrotModel>().ToTable("Parrots");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
