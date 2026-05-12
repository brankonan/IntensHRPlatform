using AutoMapper;
using IntensHRPlatform.Application.DTOs;
using IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Domain.Entities;
namespace IntensHRPlatform.Application.Services;

public class CandidateService : ICandidateService {

    private readonly ICandidateRepository _candidateRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IMapper _mapper;

    public CandidateService(ICandidateRepository candidateRepository, ISkillRepository skillRepository, IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _skillRepository = skillRepository;
        _mapper = mapper;
    }

    public async Task<List<CandidateResponseDto>> GetAllAsync()
    {
        var candidates = await _candidateRepository.GetAllAsync();
        return _mapper.Map<List<CandidateResponseDto>>(candidates);
    }

    public async Task<CandidateResponseDto?> GetByIdAsync(int id)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id);
        return _mapper.Map<CandidateResponseDto>(candidate);
    }
    public async Task<CandidateResponseDto> CreateAsync(CreateCandidateDto dto)
    {
        if (await _candidateRepository.ExistsByEmailAsync(dto.Email))
            throw new Exception("Kandidat sa tim email-om vec postoji.");

        var candidate = new Candidate
        {
            FullName = dto.FullName,
            DateOfBirth = DateTime.SpecifyKind(dto.DateOfBirth, DateTimeKind.Utc),
            ContactNumber = dto.ContactNumber,
            Email = dto.Email
        };

        var created = await _candidateRepository.AddAsync(candidate);
        return _mapper.Map<CandidateResponseDto>(created);
    }
    public async Task AddSkillToCandidateAsync(int candidateId, int skillId)
    {
        var candidate = await _candidateRepository.GetByIdAsync(candidateId);
        if (candidate == null)
            throw new Exception("Kandidat nije pronadjen.");

        var skill = await _skillRepository.GetByIdAsync(skillId);
        if (skill == null)
            throw new Exception("Skill nije pronadjen.");
            
        if (candidate.CandidateSkills.Any(cs => cs.SkillId == skillId))
            throw new Exception("Kandidat vec ima taj skill.");

        candidate.CandidateSkills.Add(new CandidateSkill { CandidateId = candidateId, SkillId = skillId });
        await _candidateRepository.UpdateAsync(candidate);
    }

    public async Task RemoveSkillFromCandidateAsync(int candidateId, int skillId)
    {
        var candidate = await _candidateRepository.GetByIdAsync(candidateId);
        if (candidate == null)
            throw new Exception("Kandidat nije pronadjen.");

        var candidateSkill = candidate.CandidateSkills.FirstOrDefault(cs => cs.SkillId == skillId);
        if (candidateSkill == null)
            throw new Exception("Kandidat nema taj skill.");

        candidate.CandidateSkills.Remove(candidateSkill);
        await _candidateRepository.UpdateAsync(candidate);
    }

    public async Task DeleteAsync(int id)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id);
        if (candidate == null)
            throw new Exception("Kandidat nije pronadjen.");

        await _candidateRepository.DeleteAsync(id);
    }

    public async Task<List<CandidateResponseDto>> SearchAsync(string? name, List<int>? skillIds)
    {
        var candidates = await _candidateRepository.SearchAsync(name, skillIds);
        return _mapper.Map<List<CandidateResponseDto>>(candidates);
    }
}
