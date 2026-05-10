using IntensHRPlatform.Application.DTOs;

namespace IntensHRPlatform.Application.Interfaces;

public interface ICandidateService {
    Task<List<CandidateResponseDto>> GetAllAsync();
    Task<CandidateResponseDto?> GetByIdAsync(int id);
    Task<CandidateResponseDto> CreateAsync(CreateCandidateDto dto);
    Task AddSkillToCandidateAsync(int candidateId, int skillId);
    Task RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    Task DeleteAsync(int id);
    Task<List<CandidateResponseDto>> SearchAsync(string? name, List<int>? skillIds);

}
