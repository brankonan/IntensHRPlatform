using Microsoft.AspNetCore.Mvc;
using IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Application.DTOs;

namespace IntensHRPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly ICandidateService _candidateService;

    public CandidatesController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var candidate = await _candidateService.GetAllAsync();
        return Ok(candidate);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var candidate = await _candidateService.GetByIdAsync(id);
        if (candidate == null) return NotFound();
        return Ok(candidate);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCandidateDto dto)
    {
        var created = await _candidateService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}/skills/{skillId}")]
    public async Task<IActionResult> AddSkill(int id, int skillId)
    {
        await _candidateService.AddSkillToCandidateAsync(id, skillId);
        return NoContent();
    }

    [HttpDelete("{id}/skills/{skillId}")]
    public async Task<IActionResult> RemoveSkill(int id, int skillId)
    {
        await _candidateService.RemoveSkillFromCandidateAsync(id, skillId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _candidateService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] List<int>? skillIds)
    {
        var results = await _candidateService.SearchAsync(name, skillIds);
        return Ok(results);
    }


}
