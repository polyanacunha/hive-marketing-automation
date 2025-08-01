using Hive.Application.UseCases.Campaigns.CreateCampaign;
using Hive.Application.UseCases.Campaigns.ListPages;
using Hive.Application.UseCases.Campaigns.Meta.CreateCampaign.ListPages;
using Hive.Application.UseCases.Campaigns.Meta.ListPages;
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
        public async Task<ActionResult> ListPages([FromBody] ListPagesQuery command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Value);
        }

        [HttpGet("ad-accounts")]
        public async Task<ActionResult> ListAdAccounts()
        {
            var result = await _mediator.Send(new ListAdAccountsQuery());

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Value);
        }
    }
}
