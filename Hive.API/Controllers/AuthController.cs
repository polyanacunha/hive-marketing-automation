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

namespace Hive.API.Controllers;

[Route("api/auth/")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(ISender mediator, SignInManager<ApplicationUser> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(new { Id = result, Message = "Um link de confirmacao foi enviado ao seu email"});
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserQuery request)
    {
        await _mediator.Send(request);
        return Ok();

    }

    [HttpPost("login/google")]
    public async Task<IResult> GoogleLogin([FromQuery] string ReturnUrl, LinkGenerator linkGenerator)
    {
        var propeties = _signInManager.ConfigureExternalAuthenticationProperties("Google",
            linkGenerator.GetPathByName(HttpContext, "GoogleLoginCallback") + $"?returnUrl={ReturnUrl}");

        return Results.Challenge(propeties, ["Google"]);
    }

    [HttpGet("login/google/callback", Name = "GoogleLoginCallback")]
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

    [HttpPost("confirm/email")]
    public async Task<ActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost("forgot/password")]
    public async Task<ActionResult> ForgotPassword([FromQuery] ForgotPasswordCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost("reset/password")]
    public async Task<ActionResult> ResetPassword([FromQuery] ResetPasswordCommand request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}

