using AutoMapper;
using IntensHRPlatform.Application.DTOs;
using IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Domain.Entities;

namespace IntensHRPlatform.Application.Services;

public class SkillService : ISkillService 
{
    private readonly ISkillRepository _skillRepository;
    private readonly IMapper _mapper;

    public SkillService(ISkillRepository skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository;
        _mapper = mapper;
    }

    public async Task<List<SkillResponseDto>> GetAllAsync()
    {
        var skills = await _skillRepository.GetAllAsync();
        return _mapper.Map<List<SkillResponseDto>>(skills);       
    }

    public async Task<SkillResponseDto> CreateAsync(CreateSkillDto dto)
    {
        if (await _skillRepository.ExistsByNameAsync(dto.Name))
            throw new Exception("Skill sa tim imenom vec postoji.");

        var skill = new Skill {  Name = dto.Name };
        var created = await _skillRepository.AddAsync(skill);
        return _mapper.Map<SkillResponseDto>(created);
    }




}
