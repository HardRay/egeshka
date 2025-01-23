namespace Egeshka.Auth.Application.Models.Repository;

public sealed record SessionUpdateModel(
    long Id,
    string AccessToken,
    string RefreshToken);
