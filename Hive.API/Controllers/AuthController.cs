using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MediatR;
using Hive.Application.UseCases.Authentication.Command;
using Hive.Application.UseCases.Authentication.Login;
using Microsoft.AspNetCore.Identity;
using Hive.Infra.Data.Identity;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Hive.Application.UseCases.Authentication.LoginWithGoogle;
using Hive.Application.UseCases.Authentication.ConfirmEmail;
using Hive.Application.UseCases.Authentication.ForgotPassword;
using Hive.Application.UseCases.Authentication.RecoverPassword;
using MimeKit.Cryptography;
using Hive.Application.UseCases.Authentication.RefreshToken;
using Google.Rpc;
using System.Text.Json;
using Hive.Application.DTOs;
using Hive.Infra.Data.Options;
using Microsoft.Extensions.Options;
using Hive.Application.UseCases.Authentication.SaveMetaAccessToken;

namespace Hive.API.Controllers;

[Route("api/auth/")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly MetaApiSettings _metaApiSettings;

    public AuthController(ISender mediator, SignInManager<ApplicationUser> signInManager, IOptions<MetaApiSettings> metaApiSettings)
    {
        _mediator = mediator;
        _signInManager = signInManager;
        _metaApiSettings = metaApiSettings.Value;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.IsFailure)
        {
            return BadRequest(new {Errors = result.Errors});
        }
        return Ok(new { Id = result.Value, Message = "Um link de confirmacao foi enviado ao seu email"});
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserQuery request)
    {
        var result = await _mediator.Send(request);
        if (result.IsFailure) 
        {
            return BadRequest(new {Errors = result.Errors});
        }
        return Ok();

    }

    [HttpPost("login-google")]
    public async Task<IResult> GoogleLogin([FromQuery] string ReturnUrl, LinkGenerator linkGenerator)
    {
        var propeties = _signInManager.ConfigureExternalAuthenticationProperties("Google",
            linkGenerator.GetPathByName(HttpContext, "GoogleLoginCallback") + $"?returnUrl={ReturnUrl}");

        return Results.Challenge(propeties, ["Google"]);
    }

    [HttpGet("login/google-callback", Name = "GoogleLoginCallback")]
    public async Task<IResult> GoogleLoginCallback([FromQuery] string ReturnUrl)
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            return Results.Unauthorized();
        }

        await _mediator.Send(new LoginWithGoogleQuery(result.Principal.FindFirstValue(ClaimTypes.Email)));

        return Results.Redirect(ReturnUrl);
    }

    [HttpPost("confirm-email")]
    public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand request)
    {
        var result = await _mediator.Send(request);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Errors = result.Errors });
        }
        return Ok();
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordCommand request)
    {
        var result = await _mediator.Send(request);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Errors = result.Errors });
        }
        return Ok(new {Message = result.Value});
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
    {
        var result = await _mediator.Send(request);
        if (!result.IsFailure) 
        {
            return BadRequest(new {Errors = result.Errors});
        } 
        return Ok();
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        if (!result.IsFailure)
        {
            return BadRequest(new { Errors = result.Errors });
        }
        return Ok();
    }

    [HttpGet("login/facebook")]
    public async Task<IActionResult> FacebookLogin()
    {
        var state = Guid.NewGuid().ToString();

        var authUrl = $"{_metaApiSettings.FacebookUrlBase}/dialog/oauth" +
                      $"?client_id={_metaApiSettings.ClientId}" +
                      $"&redirect_uri={Uri.EscapeDataString(_metaApiSettings.RedirectUri)}" +
                      $"&state={state}" +
                      $"&response_type=code" +
                      $"&scope={Uri.EscapeDataString(_metaApiSettings.Scopes)}";

        return Redirect(authUrl);
    }

    [HttpGet("facebook/callback")]
    public async Task<IActionResult> FacebookCallback([FromQuery] string Code, [FromQuery] string State)
    {
        if (string.IsNullOrEmpty(Code))
            return BadRequest("Código de autorização ausente.");

        var command = new SaveMetaAccessTokenCommand(Code);
        var result = await _mediator.Send(command);

        if(result.IsFailure)
        {
            return BadRequest(new { Errors = result.Errors });
        }

        return Ok();
    }
}

