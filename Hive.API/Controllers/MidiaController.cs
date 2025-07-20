using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Application.UseCases.Midia.CreateVideo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;

namespace Hive.API.Controllers;

[Route("api/media")]
[ApiController]
public class MidiaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MidiaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-video")]
    public async Task<ActionResult> CreateVideo([FromBody] CreateVideoCommand command)
    {
        var result = await _mediator.Send(command);

        if(result.IsFailure)
        {
            return BadRequest(new {Errors = result.Errors});
        }

        return Created("", new { jobId = result.Value });

    }
}
