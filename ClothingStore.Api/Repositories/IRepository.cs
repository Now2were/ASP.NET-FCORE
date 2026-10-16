using ClothingStore.Api.Common;
namespace ClothingStore.Api.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<PagedResult<TEntity>> GetPageAsync(int page, int pageSize, CancellationToken ct);
    Task<TEntity?> GetAsync(object[] keys, CancellationToken ct);
    Task ValidateAsync(TEntity entity, CancellationToken ct);
    void Add(TEntity entity);
    void Remove(TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken ct);
}
