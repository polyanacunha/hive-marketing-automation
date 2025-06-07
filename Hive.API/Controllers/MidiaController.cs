using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hive.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MidiaController : ControllerBase
{
    private readonly IMidiaService _midiaService;
    public MidiaController(IMidiaService midiaService)
    {
        _midiaService = midiaService;
    }

    [HttpGet("{id}", Name = "GetMidia")]
    public async Task<ActionResult<MidiaDTO>> Get(int id)
    {
        var midiaDto = await _midiaService.GetByIdAsync(id);
        if (midiaDto == null)
        {
            return NotFound("Midia not found");
        }
        return Ok(midiaDto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] MidiaDTO midiaDto)
    {
        if (id != midiaDto.Id)
        {
            return BadRequest("Data invalid");
        }

        if (midiaDto == null)
            return BadRequest("Data invalid");

        await _midiaService.UpdateAsync(midiaDto);

        return Ok(midiaDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MidiaDTO>> Delete(int id)
    {
        var midiaDto = await _midiaService.GetByIdAsync(id);

        if (midiaDto == null)
        {
            return NotFound("Midia not found");
        }

        await _midiaService.RemoveAsync(id);

        return Ok(midiaDto);
    }
}
