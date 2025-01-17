using Egeshka.ApiGateway.Dtos;
using Egeshka.ApiGateway.Dtos.UserLogin;
using Microsoft.AspNetCore.Mvc;

namespace Egeshka.ApiGateway.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.RegistrationToken) && string.IsNullOrEmpty(request.RefreshToken))
            return BadRequest(ProblemDetailsModel.Create("1000", $"Должено быть заполнено одно из полей: {nameof(UserLoginRequest.RegistrationToken)} или {nameof(UserLoginRequest.RefreshToken)}"));

        if (!string.IsNullOrEmpty(request.RegistrationToken) && !string.IsNullOrEmpty(request.RefreshToken))
            return BadRequest(ProblemDetailsModel.Create("1000", $"Должено быть заполнено только одно из полей: {nameof(UserLoginRequest.RegistrationToken)} или {nameof(UserLoginRequest.RefreshToken)}"));

        return Ok(UserLoginResponse.Generate());
    }
}
