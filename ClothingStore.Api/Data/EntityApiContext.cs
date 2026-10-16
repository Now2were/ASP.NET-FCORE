using ClothingStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Data;

/// <summary>Один контекст для всех репозиториев: конфигурация существующей БД взята из scaffold.</summary>
public class EntityApiContext(DbContextOptions<EntityApiContext> options) : ClothingStoreDbContext(options)
{
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<StockReservation> StockReservations => Set<StockReservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Сравнение исходных значений предотвращает потерю обновления при конкурирующих запросах.
        modelBuilder.Entity<ProductVariant>().Property(x => x.StockQuantity).IsConcurrencyToken();
        modelBuilder.Entity<ProductVariant>().Property(x => x.ReservedQuantity).IsConcurrencyToken();
        modelBuilder.Entity<StockReservation>(entity =>
        {
            entity.ToTable("StockReservations", table => table.HasCheckConstraint(
                "CK_StockReservations_Quantity", "[Quantity] > 0"));
            entity.HasKey(x => x.Id);
            entity.Property(x => x.CancelledAt).IsConcurrencyToken();
            entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(x => x.Variant).WithMany().HasForeignKey(x => x.VariantId).OnDelete(DeleteBehavior.NoAction);
            entity.HasIndex(x => new { x.UserId, x.VariantId, x.CancelledAt });
        });
    }
}
