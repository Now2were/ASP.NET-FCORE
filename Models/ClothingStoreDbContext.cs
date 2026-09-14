using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Models;

public partial class ClothingStoreDbContext : DbContext
{
    public ClothingStoreDbContext()
    {
    }

    public ClothingStoreDbContext(DbContextOptions<ClothingStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductMaterial> ProductMaterials { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MYBEAUTYGIRL\\SQLEXPRESS;Initial Catalog=ClothingStoreDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Addresses_UserId");

            entity.Property(e => e.Apartment).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country)
                .HasMaxLength(60)
                .HasDefaultValue("Россия");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.House).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PostIndex).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.Street).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Addresses_Users");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasIndex(e => e.Name, "UX_Brands_Name").IsUnique();

            entity.HasIndex(e => e.Slug, "UX_Brands_Slug").IsUnique();

            entity.Property(e => e.Country).HasMaxLength(60);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.LogoUrl).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.Slug).HasMaxLength(150);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.GuestToken, "UX_Carts_Guest")
                .IsUnique()
                .HasFilter("([GuestToken] IS NOT NULL)");

            entity.HasIndex(e => e.UserId, "UX_Carts_User")
                .IsUnique()
                .HasFilter("([UserId] IS NOT NULL)");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.UpdatedAt).HasPrecision(0);

            entity.HasOne(d => d.User).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Carts_Users");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasIndex(e => e.CartId, "IX_CartItems_CartId");

            entity.HasIndex(e => new { e.CartId, e.VariantId }, "UX_CartItems_CartVariant").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_CartItems_Carts");

            entity.HasOne(d => d.Variant).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.VariantId)
                .HasConstraintName("FK_CartItems_Variants");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.ParentCategoryId, "IX_Categories_Parent");

            entity.HasIndex(e => e.Slug, "UX_Categories_Slug").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.Slug).HasMaxLength(150);

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK_Categories_Parent");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasIndex(e => e.Code, "UX_Colors_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Hex)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(60);
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.HasIndex(e => e.Code, "UX_DeliveryMethods_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => new { e.StatusId, e.CreatedAt }, "IX_Orders_StatusId").IsDescending(false, true);

            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.HasIndex(e => e.OrderNumber, "UX_Orders_OrderNumber").IsUnique();

            entity.Property(e => e.Apartment).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.Country)
                .HasMaxLength(60)
                .HasDefaultValue("Россия");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasDefaultValue("RUB");
            entity.Property(e => e.DeliveryCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.House).HasMaxLength(50);
            entity.Property(e => e.OrderNumber).HasMaxLength(30);
            entity.Property(e => e.PostIndex).HasMaxLength(10);
            entity.Property(e => e.RecipientEmail).HasMaxLength(256);
            entity.Property(e => e.RecipientName).HasMaxLength(150);
            entity.Property(e => e.RecipientPhone).HasMaxLength(20);
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.Street).HasMaxLength(200);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TrackingNumber).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_DeliveryMethods");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderStatuses");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderItems_OrderId");

            entity.HasIndex(e => e.VariantId, "IX_OrderItems_VariantId");

            entity.Property(e => e.ColorHex)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ColorName).HasMaxLength(60);
            entity.Property(e => e.LineTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.SizeName).HasMaxLength(20);
            entity.Property(e => e.Sku).HasMaxLength(50);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderItems_Orders");

            entity.HasOne(d => d.Variant).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_OrderItems_Variants");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasIndex(e => e.Code, "UX_OrderStatuses_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_Payments_OrderId");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .HasDefaultValue("RUB");
            entity.Property(e => e.ExternalTransactionId).HasMaxLength(100);
            entity.Property(e => e.PaidAt).HasPrecision(0);

            entity.HasOne(d => d.Method).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Methods");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Payments_Orders");

            entity.HasOne(d => d.Status).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Statuses");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasIndex(e => e.Code, "UX_PaymentMethods_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasIndex(e => e.Code, "UX_PaymentStatuses_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.BrandId, "IX_Products_BrandId");

            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.HasIndex(e => new { e.CategoryId, e.IsActive, e.SalePrice, e.Price }, "IX_Products_Category_Price");

            entity.HasIndex(e => e.Slug, "UX_Products_Slug").IsUnique();

            entity.Property(e => e.AverageRating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SalePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Brands");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductImages_ProductId");

            entity.HasIndex(e => e.ProductId, "UX_ProductImages_OneMain")
                .IsUnique()
                .HasFilter("([IsMain]=(1))");

            entity.Property(e => e.AltText).HasMaxLength(200);
            entity.Property(e => e.ImageUrl).HasMaxLength(300);

            entity.HasOne(d => d.Product).WithOne(p => p.ProductImage)
                .HasForeignKey<ProductImage>(d => d.ProductId)
                .HasConstraintName("FK_ProductImages_Products");
        });

        modelBuilder.Entity<ProductMaterial>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductMaterials_ProductId");

            entity.Property(e => e.MaterialName).HasMaxLength(100);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductMaterials)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductMaterials_Products");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_ProductVariants_ProductId");

            entity.HasIndex(e => new { e.ProductId, e.SizeId, e.ColorId }, "UX_ProductVariants_Combination").IsUnique();

            entity.HasIndex(e => e.Sku, "UX_ProductVariants_Sku").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PriceOverride).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sku).HasMaxLength(50);

            entity.HasOne(d => d.Color).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductVariants_Colors");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductVariants_Products");

            entity.HasOne(d => d.Size).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductVariants_Sizes");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trg_Reviews_RefreshProductRating"));

            entity.HasIndex(e => e.ProductId, "IX_Reviews_ProductId");

            entity.HasIndex(e => new { e.ProductId, e.UserId }, "UX_Reviews_ProductUser").IsUnique();

            entity.Property(e => e.Comment).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Title).HasMaxLength(150);

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Reviews_Products");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Reviews_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.Name, "UX_Roles_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasIndex(e => e.Name, "UX_Sizes_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.PhoneNumber, "IX_Users_Phone").HasFilter("([PhoneNumber] IS NOT NULL)");

            entity.HasIndex(e => e.Email, "UX_Users_Email").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginAt).HasPrecision(0);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRoles_Roles"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRoles_Users"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
