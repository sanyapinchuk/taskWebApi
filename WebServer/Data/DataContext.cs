using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fridge_Product>()
            .HasOne(f => f.Fridge)
            .WithMany(fp => fp.Fridge_Products)
            .HasForeignKey(fi => fi.FridgeId);

            modelBuilder.Entity<Fridge_Product>()
            .HasOne(p => p.Product)
            .WithMany(fp=> fp.Fridge_Products)
            .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<Fridge>()
            .HasOne(m => m.FridgeModel)
            .WithMany(fm => fm.Fridges)
            .HasForeignKey(fk => fk.FridgeModelId);
        }

        public DbSet<Fridge> Fridges { get; set; }
        
        public  DbSet<Product> Products { get; set; }
        public DbSet<Fridge_Product> Fridges_Products { get; set; }
        public DbSet<FridgeModel> FridgeModels { get; set; }

    }
}
