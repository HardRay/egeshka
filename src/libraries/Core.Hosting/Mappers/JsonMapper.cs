using System.Text.Json;

namespace Egeshka.Core.Hosting.Mappers;

public static class JsonMapper
{
    public static string ToJson<T>(this T model)
        => JsonSerializer.Serialize(model);
}
