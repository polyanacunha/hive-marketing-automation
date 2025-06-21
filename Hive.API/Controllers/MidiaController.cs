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
    public MidiaController()
    {
        
    }


    [HttpPost("video")]
    public async Task<ActionResult> CreateVideo([FromForm] string id, List<IFormFile> files)
    {
        
        
        return Ok();

    }


  
}
