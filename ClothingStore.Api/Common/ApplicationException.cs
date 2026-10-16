namespace ClothingStore.Api.Common;

public enum ErrorKind { Validation, NotFound, Conflict }

/// <summary>Ошибка приложения без зависимости от HTTP.</summary>
public sealed class ApplicationException(ErrorKind kind, string message, Exception? inner = null)
    : Exception(message, inner)
{
    public ErrorKind Kind { get; } = kind;
}

public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount);
