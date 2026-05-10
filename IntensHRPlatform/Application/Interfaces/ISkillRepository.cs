namespace IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Domain.Entities;

public interface ISkillRepository
{

Task<List<Skill>> GetAllAsync();

Task<Skill?> GetByIdAsync(int skillId);

Task<Skill> AddAsync(Skill skill);

Task<bool> ExistsByNameAsync(string name); 
    
}
