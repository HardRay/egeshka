namespace Egeshka.Auth.Application.Models.Repository;

public sealed record SessionInsertModel(
    long UserId,
    string AccessToken,
    string RefreshToken);
