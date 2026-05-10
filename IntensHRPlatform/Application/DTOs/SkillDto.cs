namespace IntensHRPlatform.Application.DTOs;

public class CreateSkillDto {
    
    public string Name { get; set; } = string.Empty;
}

public class SkillResponseDto {

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

}
