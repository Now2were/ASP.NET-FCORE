using AutoMapper;
using ClothingStore.Api.Mapping;
using ClothingStore.Api.DTO;
using ClothingStore.Api.Models;
using ClothingStore.Api.Repositories;
using ClothingStore.Api.Services;
namespace ClothingStore.Api;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayers(this IServiceCollection services, IConfiguration configuration)
    {
        // Конфигурация неизменяема и общая; сам IMapper имеет Scoped lifetime.
        services.AddSingleton(sp => new MapperConfiguration(cfg =>
        {
            cfg.LicenseKey = configuration["AutoMapper:LicenseKey"];
            cfg.AddProfile<MappingProfile>();
        }, sp.GetRequiredService<ILoggerFactory>()));
        // Все слои Scoped: один HTTP-запрос получает один DbContext и общий change tracker.
        services.AddScoped<IMapper>(sp => sp.GetRequiredService<MapperConfiguration>().CreateMapper(sp.GetService));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IService<AddressDto, CreateAddressDto, UpdateAddressDto, PatchAddressDto>, Service<Address, AddressDto, CreateAddressDto, UpdateAddressDto, PatchAddressDto>>();
        services.AddScoped<IService<BrandDto, CreateBrandDto, UpdateBrandDto, PatchBrandDto>, Service<Brand, BrandDto, CreateBrandDto, UpdateBrandDto, PatchBrandDto>>();
        services.AddScoped<IService<CartDto, CreateCartDto, UpdateCartDto, PatchCartDto>, Service<Cart, CartDto, CreateCartDto, UpdateCartDto, PatchCartDto>>();
        services.AddScoped<IService<CartItemDto, CreateCartItemDto, UpdateCartItemDto, PatchCartItemDto>, Service<CartItem, CartItemDto, CreateCartItemDto, UpdateCartItemDto, PatchCartItemDto>>();
        services.AddScoped<IService<CategoryDto, CreateCategoryDto, UpdateCategoryDto, PatchCategoryDto>, Service<Category, CategoryDto, CreateCategoryDto, UpdateCategoryDto, PatchCategoryDto>>();
        services.AddScoped<IService<ColorDto, CreateColorDto, UpdateColorDto, PatchColorDto>, Service<Color, ColorDto, CreateColorDto, UpdateColorDto, PatchColorDto>>();
        services.AddScoped<IService<DeliveryMethodDto, CreateDeliveryMethodDto, UpdateDeliveryMethodDto, PatchDeliveryMethodDto>, Service<DeliveryMethod, DeliveryMethodDto, CreateDeliveryMethodDto, UpdateDeliveryMethodDto, PatchDeliveryMethodDto>>();
        services.AddScoped<IService<OrderDto, CreateOrderDto, UpdateOrderDto, PatchOrderDto>, Service<Order, OrderDto, CreateOrderDto, UpdateOrderDto, PatchOrderDto>>();
        services.AddScoped<IService<OrderItemDto, CreateOrderItemDto, UpdateOrderItemDto, PatchOrderItemDto>, Service<OrderItem, OrderItemDto, CreateOrderItemDto, UpdateOrderItemDto, PatchOrderItemDto>>();
        services.AddScoped<IService<OrderStatusDto, CreateOrderStatusDto, UpdateOrderStatusDto, PatchOrderStatusDto>, Service<OrderStatus, OrderStatusDto, CreateOrderStatusDto, UpdateOrderStatusDto, PatchOrderStatusDto>>();
        services.AddScoped<IService<PaymentDto, CreatePaymentDto, UpdatePaymentDto, PatchPaymentDto>, Service<Payment, PaymentDto, CreatePaymentDto, UpdatePaymentDto, PatchPaymentDto>>();
        services.AddScoped<IService<PaymentMethodDto, CreatePaymentMethodDto, UpdatePaymentMethodDto, PatchPaymentMethodDto>, Service<PaymentMethod, PaymentMethodDto, CreatePaymentMethodDto, UpdatePaymentMethodDto, PatchPaymentMethodDto>>();
        services.AddScoped<IService<PaymentStatusDto, CreatePaymentStatusDto, UpdatePaymentStatusDto, PatchPaymentStatusDto>, Service<PaymentStatus, PaymentStatusDto, CreatePaymentStatusDto, UpdatePaymentStatusDto, PatchPaymentStatusDto>>();
        services.AddScoped<IService<ProductDto, CreateProductDto, UpdateProductDto, PatchProductDto>, Service<Product, ProductDto, CreateProductDto, UpdateProductDto, PatchProductDto>>();
        services.AddScoped<IService<ProductImageDto, CreateProductImageDto, UpdateProductImageDto, PatchProductImageDto>, Service<ProductImage, ProductImageDto, CreateProductImageDto, UpdateProductImageDto, PatchProductImageDto>>();
        services.AddScoped<IService<ProductMaterialDto, CreateProductMaterialDto, UpdateProductMaterialDto, PatchProductMaterialDto>, Service<ProductMaterial, ProductMaterialDto, CreateProductMaterialDto, UpdateProductMaterialDto, PatchProductMaterialDto>>();
        services.AddScoped<IService<ProductVariantDto, CreateProductVariantDto, UpdateProductVariantDto, PatchProductVariantDto>, Service<ProductVariant, ProductVariantDto, CreateProductVariantDto, UpdateProductVariantDto, PatchProductVariantDto>>();
        services.AddScoped<IService<ReviewDto, CreateReviewDto, UpdateReviewDto, PatchReviewDto>, Service<Review, ReviewDto, CreateReviewDto, UpdateReviewDto, PatchReviewDto>>();
        services.AddScoped<IService<RoleDto, CreateRoleDto, UpdateRoleDto, PatchRoleDto>, Service<Role, RoleDto, CreateRoleDto, UpdateRoleDto, PatchRoleDto>>();
        services.AddScoped<IService<SizeDto, CreateSizeDto, UpdateSizeDto, PatchSizeDto>, Service<Size, SizeDto, CreateSizeDto, UpdateSizeDto, PatchSizeDto>>();
        services.AddScoped<IService<UserDto, CreateUserDto, UpdateUserDto, PatchUserDto>, Service<User, UserDto, CreateUserDto, UpdateUserDto, PatchUserDto>>();
        services.AddScoped<IService<UserRoleDto, CreateUserRoleDto, UpdateUserRoleDto, PatchUserRoleDto>, UserRoleService>();
        services.AddScoped<IStockReservationService, StockReservationService>();
        return services;
    }
}
