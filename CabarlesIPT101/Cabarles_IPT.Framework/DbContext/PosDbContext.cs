using Cabarles_IPT.Framework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Cabarles_IPT.Framework.DbContext
{
    public class PosDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options)
        {
        }

        public DbSet<OrderItemDTO> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItemDTO>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ItemName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.PricePerItem).HasColumnType("decimal(18,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
