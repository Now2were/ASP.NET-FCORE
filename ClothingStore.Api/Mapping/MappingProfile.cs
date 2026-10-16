using AutoMapper;
using ClothingStore.Api.DTO;
using ClothingStore.Api.Models;
namespace ClothingStore.Api.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressDto>();
        CreateMap<Address, AddressSummaryDto>();
        CreateMap<Address, UpdateAddressDto>();
        CreateMap<CreateAddressDto, Address>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
        CreateMap<UpdateAddressDto, Address>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
        CreateMap<PatchAddressDto, Address>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Brand, BrandDto>();
        CreateMap<Brand, BrandSummaryDto>();
        CreateMap<Brand, UpdateBrandDto>();
        CreateMap<CreateBrandDto, Brand>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Products, o => o.Ignore());
        CreateMap<UpdateBrandDto, Brand>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Products, o => o.Ignore());
        CreateMap<PatchBrandDto, Brand>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Products, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Cart, CartDto>();
        CreateMap<Cart, CartSummaryDto>();
        CreateMap<Cart, UpdateCartDto>();
        CreateMap<CreateCartDto, Cart>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.CartItems, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
        CreateMap<UpdateCartDto, Cart>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.CartItems, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
        CreateMap<PatchCartDto, Cart>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.CartItems, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<CartItem, CartItemDto>();
        CreateMap<CartItem, CartItemSummaryDto>();
        CreateMap<CartItem, UpdateCartItemDto>();
        CreateMap<CreateCartItemDto, CartItem>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Cart, o => o.Ignore())
            .ForMember(x => x.Variant, o => o.Ignore());
        CreateMap<UpdateCartItemDto, CartItem>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Cart, o => o.Ignore())
            .ForMember(x => x.Variant, o => o.Ignore());
        CreateMap<PatchCartItemDto, CartItem>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Cart, o => o.Ignore())
            .ForMember(x => x.Variant, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategorySummaryDto>();
        CreateMap<Category, UpdateCategoryDto>();
        CreateMap<CreateCategoryDto, Category>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.InverseParentCategory, o => o.Ignore())
            .ForMember(x => x.ParentCategory, o => o.Ignore())
            .ForMember(x => x.Products, o => o.Ignore());
        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.InverseParentCategory, o => o.Ignore())
            .ForMember(x => x.ParentCategory, o => o.Ignore())
            .ForMember(x => x.Products, o => o.Ignore());
        CreateMap<PatchCategoryDto, Category>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.InverseParentCategory, o => o.Ignore())
            .ForMember(x => x.ParentCategory, o => o.Ignore())
            .ForMember(x => x.Products, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Color, ColorDto>();
        CreateMap<Color, ColorSummaryDto>();
        CreateMap<Color, UpdateColorDto>();
        CreateMap<CreateColorDto, Color>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore());
        CreateMap<UpdateColorDto, Color>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore());
        CreateMap<PatchColorDto, Color>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<DeliveryMethod, DeliveryMethodDto>();
        CreateMap<DeliveryMethod, DeliveryMethodSummaryDto>();
        CreateMap<DeliveryMethod, UpdateDeliveryMethodDto>();
        CreateMap<CreateDeliveryMethodDto, DeliveryMethod>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore());
        CreateMap<UpdateDeliveryMethodDto, DeliveryMethod>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore());
        CreateMap<PatchDeliveryMethodDto, DeliveryMethod>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderSummaryDto>();
        CreateMap<Order, UpdateOrderDto>();
        CreateMap<CreateOrderDto, Order>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.DeliveryMethod, o => o.Ignore())
            .ForMember(x => x.OrderItems, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore())
            .ForMember(x => x.Status, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
        CreateMap<UpdateOrderDto, Order>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.DeliveryMethod, o => o.Ignore())
            .ForMember(x => x.OrderItems, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore())
            .ForMember(x => x.Status, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
        CreateMap<PatchOrderDto, Order>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.DeliveryMethod, o => o.Ignore())
            .ForMember(x => x.OrderItems, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore())
            .ForMember(x => x.Status, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItem, OrderItemSummaryDto>();
        CreateMap<OrderItem, UpdateOrderItemDto>();
        CreateMap<CreateOrderItemDto, OrderItem>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Order, o => o.Ignore())
            .ForMember(x => x.Variant, o => o.Ignore());
        CreateMap<UpdateOrderItemDto, OrderItem>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Order, o => o.Ignore())
            .ForMember(x => x.Variant, o => o.Ignore());
        CreateMap<PatchOrderItemDto, OrderItem>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Order, o => o.Ignore())
            .ForMember(x => x.Variant, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<OrderStatus, OrderStatusDto>();
        CreateMap<OrderStatus, OrderStatusSummaryDto>();
        CreateMap<OrderStatus, UpdateOrderStatusDto>();
        CreateMap<CreateOrderStatusDto, OrderStatus>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore());
        CreateMap<UpdateOrderStatusDto, OrderStatus>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore());
        CreateMap<PatchOrderStatusDto, OrderStatus>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Payment, PaymentDto>();
        CreateMap<Payment, PaymentSummaryDto>();
        CreateMap<Payment, UpdatePaymentDto>();
        CreateMap<CreatePaymentDto, Payment>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Method, o => o.Ignore())
            .ForMember(x => x.Order, o => o.Ignore())
            .ForMember(x => x.Status, o => o.Ignore());
        CreateMap<UpdatePaymentDto, Payment>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Method, o => o.Ignore())
            .ForMember(x => x.Order, o => o.Ignore())
            .ForMember(x => x.Status, o => o.Ignore());
        CreateMap<PatchPaymentDto, Payment>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Method, o => o.Ignore())
            .ForMember(x => x.Order, o => o.Ignore())
            .ForMember(x => x.Status, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<PaymentMethod, PaymentMethodDto>();
        CreateMap<PaymentMethod, PaymentMethodSummaryDto>();
        CreateMap<PaymentMethod, UpdatePaymentMethodDto>();
        CreateMap<CreatePaymentMethodDto, PaymentMethod>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore());
        CreateMap<UpdatePaymentMethodDto, PaymentMethod>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore());
        CreateMap<PatchPaymentMethodDto, PaymentMethod>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<PaymentStatus, PaymentStatusDto>();
        CreateMap<PaymentStatus, PaymentStatusSummaryDto>();
        CreateMap<PaymentStatus, UpdatePaymentStatusDto>();
        CreateMap<CreatePaymentStatusDto, PaymentStatus>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore());
        CreateMap<UpdatePaymentStatusDto, PaymentStatus>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore());
        CreateMap<PatchPaymentStatusDto, PaymentStatus>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Payments, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductSummaryDto>();
        CreateMap<Product, UpdateProductDto>();
        CreateMap<CreateProductDto, Product>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.Brand, o => o.Ignore())
            .ForMember(x => x.Category, o => o.Ignore())
            .ForMember(x => x.ProductImages, o => o.Ignore())
            .ForMember(x => x.ProductMaterials, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore())
            .ForMember(x => x.Reviews, o => o.Ignore());
        CreateMap<UpdateProductDto, Product>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.Brand, o => o.Ignore())
            .ForMember(x => x.Category, o => o.Ignore())
            .ForMember(x => x.ProductImages, o => o.Ignore())
            .ForMember(x => x.ProductMaterials, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore())
            .ForMember(x => x.Reviews, o => o.Ignore());
        CreateMap<PatchProductDto, Product>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.UpdatedAt, o => o.Ignore())
            .ForMember(x => x.Brand, o => o.Ignore())
            .ForMember(x => x.Category, o => o.Ignore())
            .ForMember(x => x.ProductImages, o => o.Ignore())
            .ForMember(x => x.ProductMaterials, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore())
            .ForMember(x => x.Reviews, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<ProductImage, ProductImageDto>();
        CreateMap<ProductImage, ProductImageSummaryDto>();
        CreateMap<ProductImage, UpdateProductImageDto>();
        CreateMap<CreateProductImageDto, ProductImage>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore());
        CreateMap<UpdateProductImageDto, ProductImage>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore());
        CreateMap<PatchProductImageDto, ProductImage>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<ProductMaterial, ProductMaterialDto>();
        CreateMap<ProductMaterial, ProductMaterialSummaryDto>();
        CreateMap<ProductMaterial, UpdateProductMaterialDto>();
        CreateMap<CreateProductMaterialDto, ProductMaterial>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore());
        CreateMap<UpdateProductMaterialDto, ProductMaterial>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore());
        CreateMap<PatchProductMaterialDto, ProductMaterial>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<ProductVariant, ProductVariantDto>().ForMember(x => x.AvailableQuantity, o => o.MapFrom(x => x.StockQuantity - x.ReservedQuantity));
        CreateMap<ProductVariant, ProductVariantSummaryDto>();
        CreateMap<ProductVariant, UpdateProductVariantDto>();
        CreateMap<CreateProductVariantDto, ProductVariant>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ReservedQuantity, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.CartItems, o => o.Ignore())
            .ForMember(x => x.Color, o => o.Ignore())
            .ForMember(x => x.OrderItems, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForMember(x => x.Size, o => o.Ignore());
        CreateMap<UpdateProductVariantDto, ProductVariant>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ReservedQuantity, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.CartItems, o => o.Ignore())
            .ForMember(x => x.Color, o => o.Ignore())
            .ForMember(x => x.OrderItems, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForMember(x => x.Size, o => o.Ignore());
        CreateMap<PatchProductVariantDto, ProductVariant>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ReservedQuantity, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.CartItems, o => o.Ignore())
            .ForMember(x => x.Color, o => o.Ignore())
            .ForMember(x => x.OrderItems, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForMember(x => x.Size, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Review, ReviewDto>();
        CreateMap<Review, ReviewSummaryDto>();
        CreateMap<Review, UpdateReviewDto>();
        CreateMap<CreateReviewDto, Review>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
        CreateMap<UpdateReviewDto, Review>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
        CreateMap<PatchReviewDto, Review>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Product, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Role, RoleDto>();
        CreateMap<Role, RoleSummaryDto>();
        CreateMap<Role, UpdateRoleDto>();
        CreateMap<CreateRoleDto, Role>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Users, o => o.Ignore());
        CreateMap<UpdateRoleDto, Role>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Users, o => o.Ignore());
        CreateMap<PatchRoleDto, Role>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.Users, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<Size, SizeDto>();
        CreateMap<Size, SizeSummaryDto>();
        CreateMap<Size, UpdateSizeDto>();
        CreateMap<CreateSizeDto, Size>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore());
        CreateMap<UpdateSizeDto, Size>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore());
        CreateMap<PatchSizeDto, Size>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.ProductVariants, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<User, UserDto>();
        CreateMap<User, UserSummaryDto>();
        CreateMap<User, UpdateUserDto>();
        CreateMap<CreateUserDto, User>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Addresses, o => o.Ignore())
            .ForMember(x => x.Cart, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore())
            .ForMember(x => x.Reviews, o => o.Ignore())
            .ForMember(x => x.Roles, o => o.Ignore());
        CreateMap<UpdateUserDto, User>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Addresses, o => o.Ignore())
            .ForMember(x => x.Cart, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore())
            .ForMember(x => x.Reviews, o => o.Ignore())
            .ForMember(x => x.Roles, o => o.Ignore());
        CreateMap<PatchUserDto, User>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.Addresses, o => o.Ignore())
            .ForMember(x => x.Cart, o => o.Ignore())
            .ForMember(x => x.Orders, o => o.Ignore())
            .ForMember(x => x.Reviews, o => o.Ignore())
            .ForMember(x => x.Roles, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<UserRole, UserRoleDto>();
        CreateMap<UserRole, UserRoleSummaryDto>();
        CreateMap<UserRole, UpdateUserRoleDto>();
        CreateMap<CreateUserRoleDto, UserRole>()
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.Role, o => o.Ignore());
        CreateMap<UpdateUserRoleDto, UserRole>()
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.Role, o => o.Ignore());
        CreateMap<PatchUserRoleDto, UserRole>()
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.Role, o => o.Ignore())
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
        CreateMap<StockReservation, StockReservationDto>();
        CreateMap<CreateStockReservationDto, StockReservation>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.CancelledAt, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.Variant, o => o.Ignore());
        CreateMap<StockReservation, UpdateStockReservationDto>();
        CreateMap<UpdateStockReservationDto, StockReservation>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.CreatedAt, o => o.Ignore())
            .ForMember(x => x.CancelledAt, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore())
            .ForMember(x => x.Variant, o => o.Ignore());
        CreateMap<PatchStockReservationDto, UpdateStockReservationDto>()
            .ForAllMembers(o => o.PreCondition(src => src.HasProperty(o.DestinationMember.Name)));
    }
}
