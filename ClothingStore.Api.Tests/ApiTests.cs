using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ClothingStore.Api.Common;
using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using ClothingStore.Api.Models;
using ClothingStore.Api.Repositories;
using ClothingStore.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppException = ClothingStore.Api.Common.ApplicationException;

namespace ClothingStore.Api.Tests;

public sealed class ApiTests : IAsyncLifetime
{
    private readonly ApiFactory _factory = new();
    private HttpClient _client = null!;
    public async Task InitializeAsync()
    {
        _client = _factory.CreateClient(new() { BaseAddress = new Uri("https://localhost") });
        await _factory.InitializeAsync();
    }
    public async Task DisposeAsync()
    {
        await _factory.DropTestDatabaseAsync();
        _client.Dispose();
        await _factory.DisposeAsync();
    }
    private async Task<JsonElement> Body(HttpResponseMessage response) =>
        JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());
    private Task<HttpResponseMessage> Patch(string url, string json) =>
        _client.PatchAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

    public static IEnumerable<object[]> CrudCases()
    {
        yield return ["addresses", "{\"userId\":1,\"fullName\":\"Покупатель\",\"phone\":\"123\",\"country\":\"Россия\",\"city\":\"Москва\"}"];
        yield return ["brands", "{\"name\":\"Новый бренд\",\"slug\":\"new-brand\"}"];
        yield return ["carts", "{\"guestToken\":\"319baf7c-70bc-45cb-ae46-87b9257f56bb\"}"];
        yield return ["cart-items", "{\"cartId\":1,\"variantId\":1,\"quantity\":2}"];
        yield return ["categories", "{\"name\":\"Дочерняя\",\"slug\":\"new-category\",\"parentCategoryId\":1,\"isActive\":true}"];
        yield return ["colors", "{\"name\":\"Белый\",\"code\":\"WHT\",\"hex\":\"#FFFFFF\"}"];
        yield return ["delivery-methods", "{\"name\":\"Доставка\",\"code\":\"DELIVERY\",\"cost\":100,\"isActive\":true}"];
        yield return ["orders", "{\"orderNumber\":\"TEST-2\",\"userId\":1,\"statusId\":1,\"currencyCode\":\"RUB\",\"deliveryMethodId\":1,\"recipientName\":\"Тест\",\"recipientPhone\":\"123\",\"country\":\"Россия\",\"city\":\"Москва\"}"];
        yield return ["order-items", "{\"orderId\":1,\"variantId\":1,\"productName\":\"Футболка\",\"sku\":\"TEST-M-BLK\",\"unitPrice\":100,\"quantity\":2,\"lineTotal\":200}"];
        yield return ["order-statuses", "{\"name\":\"Отправлен\",\"code\":\"SHIPPED\"}"];
        yield return ["payments", "{\"orderId\":1,\"methodId\":1,\"statusId\":1,\"amount\":200,\"currencyCode\":\"RUB\"}"];
        yield return ["payment-methods", "{\"name\":\"Наличные\",\"code\":\"CASH\",\"isActive\":true}"];
        yield return ["payment-statuses", "{\"name\":\"Оплачен\",\"code\":\"PAID\"}"];
        yield return ["products", "{\"categoryId\":1,\"brandId\":1,\"name\":\"Товар\",\"slug\":\"new-product\",\"price\":100,\"isActive\":true}"];
        yield return ["product-images", "{\"productId\":1,\"imageUrl\":\"/three.jpg\"}"];
        yield return ["product-materials", "{\"productId\":1,\"materialName\":\"Хлопок\",\"percent\":100}"];
        yield return ["product-variants", "{\"productId\":1,\"sizeId\":2,\"colorId\":1,\"sku\":\"TEST-L-BLK\",\"stockQuantity\":20,\"isActive\":true}"];
        yield return ["reviews", "{\"productId\":1,\"userId\":1,\"rating\":5,\"comment\":\"Отлично\"}"];
        yield return ["roles", "{\"name\":\"Manager\"}"];
        yield return ["sizes", "{\"name\":\"XL\"}"];
        yield return ["users", "{\"email\":\"new@example.com\",\"passwordHash\":\"hash\",\"firstName\":\"Покупатель\",\"isActive\":true}"];
    }

    [Theory]
    [MemberData(nameof(CrudCases))]
    public async Task Every_entity_supports_create_read_put_patch_delete(string route, string json)
    {
        var url = "/api/" + route;
        var created = await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        Assert.True(created.StatusCode == HttpStatusCode.Created, await created.Content.ReadAsStringAsync());
        int id = (await Body(created)).GetProperty("id").GetInt32();
        var itemUrl = url + "/" + id;
        Assert.Equal(HttpStatusCode.OK, (await _client.GetAsync(itemUrl)).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await _client.PutAsync(itemUrl, new StringContent(json, Encoding.UTF8, "application/json"))).StatusCode);
        // Передаём реально присутствующие поля: проверяем путь AutoMapper PATCH для каждого типа.
        Assert.Equal(HttpStatusCode.NoContent, (await Patch(itemUrl, json)).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await _client.DeleteAsync(itemUrl)).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await _client.GetAsync(itemUrl)).StatusCode);
    }

    [Fact]
    public async Task All_entities_have_pagination_and_missing_record_returns_404()
    {
        var routes = new[] { "addresses", "brands", "carts", "cart-items", "categories", "colors", "delivery-methods",
            "orders", "order-items", "order-statuses", "payments", "payment-methods", "payment-statuses", "products",
            "product-images", "product-materials", "product-variants", "reviews", "roles", "sizes", "users", "user-roles", "stock-reservations" };
        foreach (var route in routes)
        {
            var response = await _client.GetAsync($"/api/{route}?page=1&pageSize=1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(int.TryParse(response.Headers.GetValues("X-Total-Count").Single(), out _));
            var id = route == "user-roles" ? "999999/999999" : "999999";
            Assert.Equal(HttpStatusCode.NotFound, (await _client.GetAsync($"/api/{route}/{id}")).StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, (await _client.GetAsync($"/api/{route}?page=0")).StatusCode);
        }
        var product = await Body(await _client.GetAsync("/api/products/1"));
        Assert.Equal("Бренд", product.GetProperty("brand").GetProperty("name").GetString());
        Assert.Equal(2, product.GetProperty("productImages").GetArrayLength());
        var users = await _client.GetStringAsync("/api/users");
        Assert.DoesNotContain("passwordHash", users);
    }

    [Fact]
    public async Task Product_CRUD_patch_presence_nullable_clearing_and_relationship_validation()
    {
        var dto = new CreateProductDto { CategoryId = 1, BrandId = 1, Name = "Новый товар", Slug = "new-item",
            Price = 200, SalePrice = 150, Description = "Описание", IsActive = true };
        var created = await _client.PostAsJsonAsync("/api/products", dto);
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);
        Assert.NotNull(created.Headers.Location);
        var item = await Body(created);
        int id = item.GetProperty("id").GetInt32();
        Assert.Equal("Бренд", item.GetProperty("brand").GetProperty("name").GetString());
        Assert.Equal(HttpStatusCode.NoContent, (await Patch($"/api/products/{id}", "{\"description\":null,\"salePrice\":null}")).StatusCode);
        item = await Body(await _client.GetAsync($"/api/products/{id}"));
        Assert.Equal(JsonValueKind.Null, item.GetProperty("description").ValueKind);
        Assert.Equal(JsonValueKind.Null, item.GetProperty("salePrice").ValueKind);
        Assert.Equal(200, item.GetProperty("price").GetDecimal());
        Assert.Equal(HttpStatusCode.BadRequest, (await Patch($"/api/products/{id}", "{\"price\":null}")).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, (await Patch($"/api/products/{id}", "{\"brandId\":999999}")).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, (await Patch($"/api/products/{id}", "{\"name\":null}")).StatusCode);
        var update = new UpdateProductDto { CategoryId = 1, BrandId = 1, Name = "Изменённый", Slug = "changed",
            Price = 300, IsActive = false };
        Assert.Equal(HttpStatusCode.NoContent, (await _client.PutAsJsonAsync($"/api/products/{id}", update)).StatusCode);
        item = await Body(await _client.GetAsync($"/api/products/{id}"));
        Assert.False(item.GetProperty("isActive").GetBoolean());
        Assert.Equal(300, item.GetProperty("price").GetDecimal());
        Assert.Equal(HttpStatusCode.NoContent, (await _client.DeleteAsync($"/api/products/{id}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await _client.DeleteAsync($"/api/products/{id}")).StatusCode);
    }

    [Fact]
    public async Task Reserve_cancel_each_save_once_and_invalid_requests_do_not_change_stock()
    {
        var dto = new CreateStockReservationDto { UserId = 1, VariantId = 1, Quantity = 3 };
        var created = await _client.PostAsJsonAsync("/api/stock-reservations", dto);
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);
        Assert.Equal(1, _factory.Counter.Count);
        int id = (await Body(created)).GetProperty("id").GetInt32();
        var variant = await Body(await _client.GetAsync("/api/product-variants/1"));
        Assert.Equal(7, variant.GetProperty("availableQuantity").GetInt32());
        Assert.Equal(3, variant.GetProperty("reservedQuantity").GetInt32());
        dto.Quantity = 8;
        var failure = await _client.PostAsJsonAsync("/api/stock-reservations", dto);
        Assert.Equal(HttpStatusCode.Conflict, failure.StatusCode);
        Assert.Contains("Недостаточно", (await Body(failure)).GetProperty("detail").GetString());
        Assert.Equal(1, _factory.Counter.Count);
        Assert.Equal(HttpStatusCode.BadRequest, (await Patch("/api/product-variants/1", "{\"stockQuantity\":2}")).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, (await Patch("/api/product-variants/1", "{\"reservedQuantity\":0}")).StatusCode);
        _factory.Counter.Reset();
        var cancelled = await _client.PostAsync($"/api/stock-reservations/{id}/cancel", null);
        Assert.Equal(HttpStatusCode.OK, cancelled.StatusCode);
        Assert.Equal(1, _factory.Counter.Count);
        Assert.NotEqual(JsonValueKind.Null, (await Body(cancelled)).GetProperty("cancelledAt").ValueKind);
        Assert.Equal(HttpStatusCode.NotFound, (await _client.PostAsync($"/api/stock-reservations/{id}/cancel", null)).StatusCode);
        variant = await Body(await _client.GetAsync("/api/product-variants/1"));
        Assert.Equal(10, variant.GetProperty("availableQuantity").GetInt32());
        Assert.Equal(HttpStatusCode.OK, (await _client.GetAsync($"/api/stock-reservations/{id}")).StatusCode);
        dto.Quantity = 0;
        Assert.Equal(HttpStatusCode.BadRequest, (await _client.PostAsJsonAsync("/api/stock-reservations", dto)).StatusCode);
        dto.Quantity = 1; dto.UserId = 999999;
        Assert.Equal(HttpStatusCode.NotFound, (await _client.PostAsJsonAsync("/api/stock-reservations", dto)).StatusCode);
    }

    [Fact]
    public async Task Reservation_update_changes_stock_and_delete_cancels_without_losing_history()
    {
        var created = await _client.PostAsJsonAsync("/api/stock-reservations", new { userId = 1, variantId = 1, quantity = 2 });
        int id = (await Body(created)).GetProperty("id").GetInt32();
        _factory.Counter.Reset();
        Assert.Equal(HttpStatusCode.NoContent, (await Patch($"/api/stock-reservations/{id}", "{\"quantity\":5}")).StatusCode);
        Assert.Equal(1, _factory.Counter.Count);
        Assert.Equal(5, (await Body(await _client.GetAsync("/api/product-variants/1"))).GetProperty("availableQuantity").GetInt32());
        Assert.Equal(HttpStatusCode.Conflict, (await Patch($"/api/stock-reservations/{id}", "{\"quantity\":11}")).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await _client.PutAsJsonAsync($"/api/stock-reservations/{id}",
            new { userId = 1, variantId = 1, quantity = 4 })).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await _client.DeleteAsync($"/api/stock-reservations/{id}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await _client.DeleteAsync($"/api/stock-reservations/{id}")).StatusCode);
        Assert.Equal(10, (await Body(await _client.GetAsync("/api/product-variants/1"))).GetProperty("availableQuantity").GetInt32());
    }

    [Fact]
    public async Task UserRole_composite_key_CRUD_and_conflicts()
    {
        var created = await _client.PostAsJsonAsync("/api/user-roles", new { userId = 1, roleId = 1 });
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, (await _client.PostAsJsonAsync("/api/user-roles", new { userId = 1, roleId = 1 })).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await Patch("/api/user-roles/1/1", "{\"roleId\":2}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await _client.GetAsync("/api/user-roles/1/1")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await _client.GetAsync("/api/user-roles/1/2")).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await _client.PutAsJsonAsync("/api/user-roles/1/2", new { userId = 1, roleId = 1 })).StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, (await _client.DeleteAsync("/api/user-roles/1/1")).StatusCode);
    }

    [Fact]
    public async Task Concurrent_reservation_rolls_back_event_and_concurrent_cancel_cannot_release_twice()
    {
        using var scope1 = _factory.Services.CreateScope();
        using var scope2 = _factory.Services.CreateScope();
        var first = scope1.ServiceProvider.GetRequiredService<IStockReservationService>();
        var second = scope2.ServiceProvider.GetRequiredService<IStockReservationService>();
        // Оба запроса читают один и тот же исходный остаток до первой записи.
        await scope1.ServiceProvider.GetRequiredService<IRepository<ProductVariant>>().GetAsync([1], default);
        await scope2.ServiceProvider.GetRequiredService<IRepository<ProductVariant>>().GetAsync([1], default);
        var dto = new CreateStockReservationDto { UserId = 1, VariantId = 1, Quantity = 7 };
        var reservation = await first.ReserveAsync(dto, default);
        var error = await Assert.ThrowsAsync<AppException>(() => second.ReserveAsync(dto, default));
        Assert.Equal(ErrorKind.Conflict, error.Kind);
        using (var verify = _factory.Services.CreateScope())
        {
            var db = verify.ServiceProvider.GetRequiredService<EntityApiContext>();
            Assert.Equal(1, await db.StockReservations.CountAsync());
            Assert.Equal(7, (await db.ProductVariants.SingleAsync()).ReservedQuantity);
        }
        using var cancel1 = _factory.Services.CreateScope();
        using var cancel2 = _factory.Services.CreateScope();
        await cancel1.ServiceProvider.GetRequiredService<IRepository<StockReservation>>().GetAsync([reservation.Id], default);
        await cancel2.ServiceProvider.GetRequiredService<IRepository<StockReservation>>().GetAsync([reservation.Id], default);
        await cancel1.ServiceProvider.GetRequiredService<IStockReservationService>().CancelAsync(reservation.Id, default);
        error = await Assert.ThrowsAsync<AppException>(() => cancel2.ServiceProvider.GetRequiredService<IStockReservationService>()
            .CancelAsync(reservation.Id, default));
        Assert.Equal(ErrorKind.Conflict, error.Kind);
        Assert.Equal(10, (await Body(await _client.GetAsync("/api/product-variants/1"))).GetProperty("availableQuantity").GetInt32());
    }

    [Fact]
    public async Task Swagger_exposes_all_entities_and_write_operations()
    {
        var response = await _client.GetAsync("/swagger/v1/swagger.json");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var paths = (await Body(response)).GetProperty("paths");
        Assert.True(paths.TryGetProperty("/api/stock-reservations/{id}/cancel", out _));
        Assert.True(paths.GetProperty("/api/products/{id}").TryGetProperty("patch", out _));
        Assert.True(paths.GetProperty("/api/user-roles/{userId}/{roleId}").TryGetProperty("put", out _));
    }
}
