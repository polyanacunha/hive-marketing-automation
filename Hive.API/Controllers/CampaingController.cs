using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hive.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CampaingController : ControllerBase
{
    private readonly ICampaingService _campaingService;
    public CampaingController(ICampaingService campaingService)
    {
        _campaingService = campaingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CampaingDTO>>> Get()
    {
        var campaings = await _campaingService.GetCampaings();
        if (campaings == null)
        {
            return NotFound("Campaings not found");
        }
        return Ok(campaings);
    }

    /// <summary>
    /// Localiza uma campanha específica pelo Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpGet("{id:int}", Name = "GetCampaing")]
    public async Task<ActionResult<CampaingDTO>> Get(int id)
    {
        var campaing = await _campaingService.GetById(id);
        if (campaing == null)
        {
            return NotFound("Campaing not found");
        }
        return Ok(campaing);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CampaingDTO campaingDto)
    {
        if (campaingDto == null)
            return BadRequest("Invalid Data");

        await _campaingService.Add(campaingDto);

        return new CreatedAtRouteResult("GetCampaing", new { id = campaingDto.Id },
            campaingDto);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] CampaingDTO campaingDto)
    {
        if (id != campaingDto.Id)
            return BadRequest();

        if (campaingDto == null)
            return BadRequest();

        await _campaingService.Update(campaingDto);

        return Ok(campaingDto);
    }

    /// <summary>
    /// Deleta uma campanha específica pelo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CampaingDTO>> Delete(int id)
    {
        var campaing = await _campaingService.GetById(id);
        if (campaing == null)
        {
            return NotFound("Campaing not found");
        }

        await _campaingService.Remove(id);

        return Ok(campaing);

    }
}
