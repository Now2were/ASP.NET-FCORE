using ClothingStore.Api.Data;
using ClothingStore.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ClothingStore.Api.Tests;

/// <summary>Изолированная временная БД SQL Server; ClothingStoreDB никогда не изменяется тестами.</summary>
public sealed class ApiFactory : WebApplicationFactory<Program>
{
    public readonly string DatabaseName = "ClothingStoreApiTests_" + Guid.NewGuid().ToString("N");
    public SaveCounter Counter { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<EntityApiContext>();
            services.RemoveAll<DbContextOptions<EntityApiContext>>();
            var server = Environment.GetEnvironmentVariable("CLOTHINGSTORE_TEST_SERVER") ?? @".\SQLEXPRESS";
            services.AddDbContext<EntityApiContext>(options => options.UseSqlServer(
                $"Server={server};Database={DatabaseName};Integrated Security=True;TrustServerCertificate=True")
                .AddInterceptors(Counter));
        });
    }

    public async Task InitializeAsync()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<EntityApiContext>();
        await db.Database.EnsureCreatedAsync();
        var category = new Category { Name = "Категория", Slug = "test-category", IsActive = true };
        var brand = new Brand { Name = "Бренд", Slug = "test-brand" };
        var color = new Color { Name = "Чёрный", Code = "BLK", Hex = "#000000" };
        var size = new Size { Name = "M" };
        var user = new User { Email = "test@example.com", PasswordHash = "test-hash", IsActive = true, CreatedAt = DateTime.UtcNow };
        var product = new Product { Name = "Футболка", Slug = "test-shirt", Price = 100, IsActive = true,
            Category = category, Brand = brand, CreatedAt = DateTime.UtcNow };
        db.AddRange(user, new Role { Name = "Customer" }, new Role { Name = "Admin" }, product,
            new ProductVariant { Product = product, Size = size, Color = color, Sku = "TEST-M-BLK",
                StockQuantity = 10, IsActive = true, CreatedAt = DateTime.UtcNow },
            new ProductImage { Product = product, ImageUrl = "/one.jpg", IsMain = true },
            new ProductImage { Product = product, ImageUrl = "/two.jpg", IsMain = false });
        await db.SaveChangesAsync();
        var delivery = new DeliveryMethod { Code = "PICKUP", Name = "Самовывоз", IsActive = true };
        var status = new OrderStatus { Code = "NEW", Name = "Новый" };
        db.AddRange(new Size { Name = "L" }, new Cart { User = user, CreatedAt = DateTime.UtcNow },
            new PaymentMethod { Code = "CARD", Name = "Карта", IsActive = true },
            new PaymentStatus { Code = "PENDING", Name = "Ожидается" },
            new Order { OrderNumber = "TEST-1", User = user, Status = status, DeliveryMethod = delivery,
                CurrencyCode = "RUB", RecipientName = "Тест", RecipientPhone = "123", Country = "Россия", City = "Москва",
                CreatedAt = DateTime.UtcNow });
        await db.SaveChangesAsync();
        Counter.Reset();
    }

    public async Task DropTestDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<EntityApiContext>();
        if (db.Database.GetDbConnection().Database != DatabaseName || !DatabaseName.StartsWith("ClothingStoreApiTests_"))
            throw new InvalidOperationException("Refusing to delete a non-test database.");
        await db.Database.EnsureDeletedAsync();
    }
}

public sealed class SaveCounter : SaveChangesInterceptor
{
    private int _count;
    public int Count => Volatile.Read(ref _count);
    public void Reset() => Interlocked.Exchange(ref _count, 0);
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        Interlocked.Increment(ref _count);
        return ValueTask.FromResult(result);
    }
}
