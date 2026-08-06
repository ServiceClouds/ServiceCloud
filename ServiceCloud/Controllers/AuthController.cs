using Application.Abstractions.Commands.Login;
using Application.Abstractions.Commands.Login.GetCompanies;
using Application.Abstractions.Commands.Login.VefityLogin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPost("get-companies")]
    public async Task<IActionResult> GetCompanies(
        GetCompaniesByEmailRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetCompaniesByEmailQuery(request.Email),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPost("verify-login")]
    public async Task<IActionResult> VerifyLogin(
        VerifyLoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}