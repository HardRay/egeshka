using System.Text.Json;

namespace Egeshka.ApiGateway.Mappers;

public static class JsonMapper
{
    public static string ToJson<T>(this T model)
        => JsonSerializer.Serialize(model);
}
