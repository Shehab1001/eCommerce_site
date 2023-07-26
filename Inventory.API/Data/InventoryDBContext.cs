using BlazorApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Data
{
    public class InventoryDBContext : DbContext
    {
        public InventoryDBContext (DbContextOptions<InventoryDBContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Product>()
                .Property(b => b.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Product>()
                .Property(b => b.Name).HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .Property(b => b.ProductType).HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .Property(b => b.Description).HasMaxLength(1000);

            modelBuilder.Entity<Product>()
                .Property(b => b.PictureUri).HasMaxLength(1000);
        }
    }
}
