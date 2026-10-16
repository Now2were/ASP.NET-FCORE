using System.Text.Json.Serialization;
namespace ClothingStore.Api.DTO;
/// <summary>Отличает отсутствующее поле от явно переданного null в merge patch.</summary>
public abstract class PatchDto
{
    private readonly HashSet<string> _provided = new(StringComparer.Ordinal);
    protected void Set<T>(ref T field, T value, string name) { field = value; _provided.Add(name); }
    public bool HasProperty(string name) => _provided.Contains(name);
    [JsonIgnore] public IReadOnlySet<string> ProvidedProperties => _provided;
}
