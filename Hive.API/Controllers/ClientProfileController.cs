using Hive.Application.UseCases.Client;
using Hive.Application.UseCases.Client.ListMarketSegment;
using Hive.Application.UseCases.Client.ListObjectiveCampaign;
using Hive.Application.UseCases.Client.ListTargetAudience;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
            var result = await _mediator.Send(request);

            if(result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok();
        }

        [HttpGet("market-segment")]
        public async Task<ActionResult> ListMarketSegment()
        {
            var result = await _mediator.Send(new ListMarketSegmentQuery());

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok(result.Value);
        }

        [HttpGet("target-audience")]
        public async Task<ActionResult> ListTargetAudience()
        {
            var result = await _mediator.Send(new ListTargetAudienceQuery());

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok(result.Value);
        }

        [HttpGet("objective-campaign")]
        public async Task<ActionResult> ListObjectiveCampaign()
        {
            var result = await _mediator.Send(new ListObjectiveCampaignQuery());

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok(result.Value);
        }
    }
}