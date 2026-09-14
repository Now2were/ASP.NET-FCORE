using ClothingStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Data;

/// <summary>EF Core-контекст базы данных ClothingStoreDB.</summary>
public class EntityApiContext : DbContext
{
    public EntityApiContext(DbContextOptions<EntityApiContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Size> Sizes => Set<Size>();
    public DbSet<Color> Colors => Set<Color>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductMaterial> ProductMaterials => Set<ProductMaterial>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();
    public DbSet<PaymentStatus> PaymentStatuses => Set<PaymentStatus>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
    public DbSet<DeliveryMethod> DeliveryMethods => Set<DeliveryMethod>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(role =>
        {
            role.ToTable("Roles");
            role.HasKey(role => role.Id);
            role.Property(role => role.Name).IsRequired().HasMaxLength(50);
            role.Property(role => role.Description).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(user =>
        {
            user.ToTable("Users");
            user.HasKey(user => user.Id);
            user.Property(user => user.Email).IsRequired().HasMaxLength(256);
            user.Property(user => user.PasswordHash).IsRequired().HasMaxLength(256);
            user.Property(user => user.FirstName).HasMaxLength(100);
            user.Property(user => user.LastName).HasMaxLength(100);
            user.Property(user => user.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Address>(address =>
        {
            address.ToTable("Addresses");
            address.HasKey(address => address.Id);
            address.Property(address => address.FullName).IsRequired().HasMaxLength(150);
            address.Property(address => address.Phone).IsRequired().HasMaxLength(20);
            address.Property(address => address.Country).IsRequired().HasMaxLength(60);
            address.Property(address => address.Region).HasMaxLength(100);
            address.Property(address => address.City).IsRequired().HasMaxLength(100);
            address.Property(address => address.Street).HasMaxLength(200);
            address.Property(address => address.House).HasMaxLength(50);
            address.Property(address => address.Apartment).HasMaxLength(50);
            address.Property(address => address.PostIndex).HasMaxLength(10);
        });

        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Categories");
            category.HasKey(category => category.Id);
            category.Property(category => category.Name).IsRequired().HasMaxLength(120);
            category.Property(category => category.Slug).IsRequired().HasMaxLength(150);
        });

        modelBuilder.Entity<Brand>(brand =>
        {
            brand.ToTable("Brands");
            brand.HasKey(brand => brand.Id);
            brand.Property(brand => brand.Name).IsRequired().HasMaxLength(120);
            brand.Property(brand => brand.Slug).IsRequired().HasMaxLength(150);
            brand.Property(brand => brand.Country).HasMaxLength(60);
            brand.Property(brand => brand.Description).HasMaxLength(500);
            brand.Property(brand => brand.LogoUrl).HasMaxLength(300);
        });

        modelBuilder.Entity<Size>(size =>
        {
            size.ToTable("Sizes");
            size.HasKey(size => size.Id);
            size.Property(size => size.Name).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<Color>(color =>
        {
            color.ToTable("Colors");
            color.HasKey(color => color.Id);
            color.Property(color => color.Name).IsRequired().HasMaxLength(60);
            color.Property(color => color.Code).IsRequired().HasMaxLength(10);
            color.Property(color => color.Hex).IsRequired().HasMaxLength(7);
        });

        modelBuilder.Entity<Product>(product =>
        {
            product.ToTable("Products");
            product.HasKey(product => product.Id);
            product.Property(product => product.Name).IsRequired().HasMaxLength(200);
            product.Property(product => product.Slug).IsRequired().HasMaxLength(200);
            product.Property(product => product.Price).HasPrecision(10, 2);
            product.Property(product => product.SalePrice).HasPrecision(10, 2);
            product.Property(product => product.AverageRating).HasPrecision(3, 2);
        });

        modelBuilder.Entity<ProductMaterial>(material =>
        {
            material.ToTable("ProductMaterials");
            material.HasKey(material => material.Id);
            material.Property(material => material.MaterialName).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<ProductImage>(image =>
        {
            image.ToTable("ProductImages");
            image.HasKey(image => image.Id);
            image.Property(image => image.ImageUrl).IsRequired().HasMaxLength(300);
            image.Property(image => image.AltText).HasMaxLength(200);
        });

        modelBuilder.Entity<ProductVariant>(variant =>
        {
            variant.ToTable("ProductVariants");
            variant.HasKey(variant => variant.Id);
            variant.Property(variant => variant.Sku).IsRequired().HasMaxLength(50);
            variant.Property(variant => variant.PriceOverride).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Review>(review =>
        {
            review.ToTable("Reviews");
            review.HasKey(review => review.Id);
            review.Property(review => review.Title).HasMaxLength(150);
            review.Property(review => review.Comment).HasMaxLength(2000);
        });

        modelBuilder.Entity<OrderStatus>(orderStatus =>
        {
            orderStatus.ToTable("OrderStatuses");
            orderStatus.HasKey(orderStatus => orderStatus.Id);
            orderStatus.Property(orderStatus => orderStatus.Code).IsRequired().HasMaxLength(30);
            orderStatus.Property(orderStatus => orderStatus.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<PaymentStatus>(paymentStatus =>
        {
            paymentStatus.ToTable("PaymentStatuses");
            paymentStatus.HasKey(paymentStatus => paymentStatus.Id);
            paymentStatus.Property(paymentStatus => paymentStatus.Code).IsRequired().HasMaxLength(30);
            paymentStatus.Property(paymentStatus => paymentStatus.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<PaymentMethod>(paymentMethod =>
        {
            paymentMethod.ToTable("PaymentMethods");
            paymentMethod.HasKey(paymentMethod => paymentMethod.Id);
            paymentMethod.Property(paymentMethod => paymentMethod.Code).IsRequired().HasMaxLength(30);
            paymentMethod.Property(paymentMethod => paymentMethod.Name).IsRequired().HasMaxLength(120);
        });

        modelBuilder.Entity<DeliveryMethod>(deliveryMethod =>
        {
            deliveryMethod.ToTable("DeliveryMethods");
            deliveryMethod.HasKey(deliveryMethod => deliveryMethod.Id);
            deliveryMethod.Property(deliveryMethod => deliveryMethod.Code).IsRequired().HasMaxLength(30);
            deliveryMethod.Property(deliveryMethod => deliveryMethod.Name).IsRequired().HasMaxLength(120);
            deliveryMethod.Property(deliveryMethod => deliveryMethod.Cost).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Order>(order =>
        {
            order.ToTable("Orders");
            order.HasKey(order => order.Id);
            order.Property(order => order.OrderNumber).IsRequired().HasMaxLength(30);
            order.Property(order => order.Subtotal).HasPrecision(10, 2);
            order.Property(order => order.DiscountAmount).HasPrecision(10, 2);
            order.Property(order => order.DeliveryCost).HasPrecision(10, 2);
            order.Property(order => order.Total).HasPrecision(10, 2);
            order.Property(order => order.CurrencyCode).IsRequired().HasMaxLength(3);
            order.Property(order => order.RecipientName).IsRequired().HasMaxLength(150);
            order.Property(order => order.RecipientPhone).IsRequired().HasMaxLength(20);
            order.Property(order => order.RecipientEmail).HasMaxLength(256);
            order.Property(order => order.Country).IsRequired().HasMaxLength(60);
            order.Property(order => order.Region).HasMaxLength(100);
            order.Property(order => order.City).IsRequired().HasMaxLength(100);
            order.Property(order => order.Street).HasMaxLength(200);
            order.Property(order => order.House).HasMaxLength(50);
            order.Property(order => order.Apartment).HasMaxLength(50);
            order.Property(order => order.PostIndex).HasMaxLength(10);
            order.Property(order => order.TrackingNumber).HasMaxLength(100);
            order.Property(order => order.Comment).HasMaxLength(500);
        });

        modelBuilder.Entity<OrderItem>(orderItem =>
        {
            orderItem.ToTable("OrderItems");
            orderItem.HasKey(orderItem => orderItem.Id);
            orderItem.Property(orderItem => orderItem.ProductName).IsRequired().HasMaxLength(200);
            orderItem.Property(orderItem => orderItem.Sku).IsRequired().HasMaxLength(50);
            orderItem.Property(orderItem => orderItem.SizeName).HasMaxLength(20);
            orderItem.Property(orderItem => orderItem.ColorName).HasMaxLength(60);
            orderItem.Property(orderItem => orderItem.ColorHex).HasMaxLength(7);
            orderItem.Property(orderItem => orderItem.UnitPrice).HasPrecision(10, 2);
            orderItem.Property(orderItem => orderItem.LineTotal).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Payment>(payment =>
        {
            payment.ToTable("Payments");
            payment.HasKey(payment => payment.Id);
            payment.Property(payment => payment.Amount).HasPrecision(10, 2);
            payment.Property(payment => payment.CurrencyCode).IsRequired().HasMaxLength(3);
            payment.Property(payment => payment.ExternalTransactionId).HasMaxLength(100);
        });

        modelBuilder.Entity<Cart>(cart =>
        {
            cart.ToTable("Carts");
            cart.HasKey(cart => cart.Id);
        });

        modelBuilder.Entity<CartItem>(cartItem =>
        {
            cartItem.ToTable("CartItems");
            cartItem.HasKey(cartItem => cartItem.Id);
        });

        modelBuilder.Entity<UserRole>(userRole =>
        {
            userRole.ToTable("UserRoles");
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
        });
    }
}