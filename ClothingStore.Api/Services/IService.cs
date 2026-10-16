using ClothingStore.Api.Common;
namespace ClothingStore.Api.Services;

public interface IService<TRead, in TCreate, in TUpdate, in TPatch>
{
    Task<PagedResult<TRead>> GetPageAsync(int page, int pageSize, CancellationToken ct);
    Task<TRead> GetAsync(object[] keys, CancellationToken ct);
    Task<TRead> CreateAsync(TCreate dto, CancellationToken ct);
    Task UpdateAsync(object[] keys, TUpdate dto, CancellationToken ct);
    Task PatchAsync(object[] keys, TPatch dto, CancellationToken ct);
    Task DeleteAsync(object[] keys, CancellationToken ct);
}
