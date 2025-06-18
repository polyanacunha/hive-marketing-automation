using Hive.Application.UseCases.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Hive.API.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientProfileController : ControllerBase
    {
        private readonly ILogger<ClientProfileController> _logger;
        private readonly ISender _mediator;

        public ClientProfileController(ILogger<ClientProfileController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("profile")]
        public async Task<ActionResult> CreateClientProfile([FromBody] CreateClientProfileCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}