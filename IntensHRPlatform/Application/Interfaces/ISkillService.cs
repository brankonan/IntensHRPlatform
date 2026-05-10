using IntensHRPlatform.Application.DTOs;

namespace IntensHRPlatform.Application.Interfaces;

public interface ISkillService {

    Task<List<SkillResponseDto>> GetAllAsync();
    Task<SkillResponseDto> CreateAsync(CreateSkillDto dto);

}
