using Microsoft.AspNetCore.Mvc;
using IntensHRPlatform.Application.DTOs;
using IntensHRPlatform.Application.Interfaces;

namespace IntensHRPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase 
{

    private readonly ISkillService _skillService;

    public SkillsController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var skills = await _skillService.GetAllAsync();
        return Ok(skills);
    }

    [HttpPost]

    public async Task<IActionResult> Create([FromBody] CreateSkillDto dto)
    {
        var created = await _skillService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), created);
    }

}
