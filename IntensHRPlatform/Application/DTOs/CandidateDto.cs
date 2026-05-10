namespace IntensHRPlatform.Application.DTOs;

public class CreateCandidateDto {

    public string FullName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string ContactNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}

public class CandidateResponseDto {
    
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string ContactNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public List<SkillResponseDto> Skills { get; set; } = new List<SkillResponseDto>();
}
