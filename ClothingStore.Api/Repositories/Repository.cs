using ClothingStore.Api.Common;
using ClothingStore.Api.Data;
using ClothingStore.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AppException = ClothingStore.Api.Common.ApplicationException;

namespace ClothingStore.Api.Repositories;

public class Repository<TEntity>(EntityApiContext context) : IRepository<TEntity> where TEntity : class
{
    private IQueryable<TEntity> ReadQuery()
    {
        IQueryable<TEntity> query = context.Set<TEntity>().AsNoTracking();
        // Один уровень связей; вложенные SummaryDto не содержат обратных навигаций.
        var metadata = context.Model.FindEntityType(typeof(TEntity))!;
        foreach (var navigation in metadata.GetNavigations()) query = query.Include(navigation.Name);
        foreach (var navigation in metadata.GetSkipNavigations()) query = query.Include(navigation.Name);
        return query.AsSplitQuery();
    }

    public async Task<PagedResult<TEntity>> GetPageAsync(int page, int pageSize, CancellationToken ct)
    {
        var total = await context.Set<TEntity>().CountAsync(ct);
        var query = ReadQuery();
        var keys = context.Model.FindEntityType(typeof(TEntity))!.FindPrimaryKey()!.Properties;
        // Все текущие ключи целочисленные; для UserRole учитываются обе части ключа.
        IOrderedQueryable<TEntity>? ordered = null;
        foreach (var key in keys)
            ordered = ordered is null
                ? query.OrderBy(x => EF.Property<int>(x, key.Name))
                : ordered.ThenBy(x => EF.Property<int>(x, key.Name));
        return new(await ordered!.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct), total);
    }

    public async Task<TEntity?> GetAsync(object[] keys, CancellationToken ct)
    {
        var entity = await context.Set<TEntity>().FindAsync(keys, ct);
        if (entity is null) return null;
        var entry = context.Entry(entity);
        foreach (var navigation in entry.Navigations)
            if (!navigation.IsLoaded) await navigation.LoadAsync(ct);
        return entity;
    }

    public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);
    public void Remove(TEntity entity) => context.Set<TEntity>().Remove(entity);

    public async Task ValidateAsync(TEntity entity, CancellationToken ct)
    {
        var metadata = context.Model.FindEntityType(typeof(TEntity))!;
        foreach (var property in metadata.GetProperties())
        {
            var value = property.PropertyInfo?.GetValue(entity);
            if (!property.IsNullable && property.ClrType == typeof(string) &&
                (value is not string text || string.IsNullOrWhiteSpace(text)))
                throw new AppException(ErrorKind.Validation, $"Поле {property.Name} обязательно.");
            if (value is string str && property.GetMaxLength() is int limit && str.Length > limit)
                throw new AppException(ErrorKind.Validation, $"Поле {property.Name}: максимум {limit} символов.");
        }
        foreach (var fk in metadata.GetForeignKeys())
        {
            var values = fk.Properties.Select(p => p.PropertyInfo!.GetValue(entity)).ToArray();
            if (values.Any(x => x is null)) continue;
            if (await context.FindAsync(fk.PrincipalEntityType.ClrType, values, ct) is null)
                throw new AppException(ErrorKind.Validation,
                    $"Связанная запись {fk.PrincipalEntityType.ClrType.Name} ({string.Join(", ", values)}) не найдена.");
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        try { return await context.SaveChangesAsync(ct); }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new AppException(ErrorKind.Conflict,
                "Данные изменены другим запросом. Обновите данные и повторите операцию.", ex);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sql &&
            sql.Number is 2601 or 2627 or 547 or 515 or 8152 or 2628 or 8115)
        {
            var message = sql.Number switch
            {
                2601 or 2627 => "Запись с такими уникальными значениями уже существует.",
                547 => "Операция нарушает связь или ограничение базы данных. Проверьте значения; перед удалением уберите зависимые записи.",
                8115 => "Числовое значение выходит за допустимый диапазон базы данных.",
                _ => "Обязательное поле не заполнено или превышена допустимая длина."
            };
            throw new AppException(sql.Number is 2601 or 2627 or 547 ? ErrorKind.Conflict : ErrorKind.Validation, message, ex);
        }
    }
}
