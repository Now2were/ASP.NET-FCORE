using AutoMapper;
using ClothingStore.Api.Common;
using ClothingStore.Api.DTO;
using ClothingStore.Api.Models;
using ClothingStore.Api.Repositories;
using AppException = ClothingStore.Api.Common.ApplicationException;
namespace ClothingStore.Api.Services;

/// <summary>Составной первичный ключ нельзя менять у tracked entity: заменяем запись связи.</summary>
public class UserRoleService(IRepository<UserRole> repository, IMapper mapper)
    : Service<UserRole, UserRoleDto, CreateUserRoleDto, UpdateUserRoleDto, PatchUserRoleDto>(repository, mapper)
{
    public override async Task UpdateAsync(object[] keys, UpdateUserRoleDto dto, CancellationToken ct)
    {
        var old = await FindAsync(keys, ct);
        if (old.UserId == dto.UserId && old.RoleId == dto.RoleId) return;
        if (await Repository.GetAsync([dto.UserId, dto.RoleId], ct) is not null)
            throw new AppException(ErrorKind.Conflict, "Такая роль уже назначена пользователю.");
        var replacement = Mapper.Map<UserRole>(dto);
        await ValidateEntityAsync(replacement, ct);
        Repository.Remove(old);
        Repository.Add(replacement);
        await Repository.SaveChangesAsync(ct);
    }

    public override async Task PatchAsync(object[] keys, PatchUserRoleDto dto, CancellationToken ct)
    {
        var old = await FindAsync(keys, ct);
        ValidatePatch(dto);
        // AutoMapper изменяет отдельный объект, не первичный ключ tracked entity.
        var replacement = Mapper.Map<UserRole>(Mapper.Map<UpdateUserRoleDto>(old));
        Mapper.Map(dto, replacement);
        await UpdateAsync(keys, Mapper.Map<UpdateUserRoleDto>(replacement), ct);
    }
}
