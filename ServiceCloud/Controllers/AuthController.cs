using Application.Abstractions.Commands.Login;
using Application.Abstractions.Commands.Login.GetCompanies;
using Application.Commands.Login;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)//provided by mediatr
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
     LoginCommand command,
     CancellationToken cancellationToken)//command obj created containg data of loginuser
    {
        var result = await _sender.Send(command, cancellationToken);//mediart search for handler and obtaon a response

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Success = false,
                Message = result.Error.Description
            });
        }

        return Ok(result.Value);
    }

    [HttpPost("get-companies")]
    public async Task<IActionResult> GetCompanies(
        GetCompaniesByEmailRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetCompaniesByEmailQuery(request.Email),
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Success = false,
                Message = result.Error.Description
            });
        }

        return Ok(result.Value);
    }
    [HttpGet("generate-hash")]
    public IActionResult GenerateHash()
    {
        var hasher = new PasswordHasher();

        string salt = "ServiceCloud@2026";

        string hash = hasher.HashPassword("Admin123", salt);

        return Ok(new
        {
            Password = "Admin123",
            Salt = salt,
            Hash = hash
        });
    }

}