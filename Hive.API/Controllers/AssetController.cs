using Hive.Application.UseCases.Midia.CreateVideo;
using Hive.Application.UseCases.Midia.GetImages;
using Hive.Application.UseCases.Midia.UploadImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hive.API.Controllers
{
    [Route("api/asset")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly ISender _mediator;

        public AssetController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadFile([FromForm] UploadImageCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok();
        }

        [HttpGet("images")]
        public async Task<ActionResult> GetAllImages([FromQuery] GetAllImagesQuery command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Value);
        }
    }
}
