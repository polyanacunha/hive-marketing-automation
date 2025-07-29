using Hive.Application.UseCases.Campaigns.CreateCampaign;
using Hive.Application.UseCases.Campaigns.ListPages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hive.API.Controllers
{
    [Route("api/campaign")]
    [ApiController]
    [Authorize]
    public class CampaignController : ControllerBase
    {
        private readonly ISender _mediator;
        public CampaignController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCampaign([FromBody] CreateCampaignCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(new { Data = result.Value });
        }

        [HttpGet("list-pages")]
        public async Task<ActionResult> ListPages()
        {
            var result = await _mediator.Send(new ListPagesQuery());

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Value);
        }
    }
}
