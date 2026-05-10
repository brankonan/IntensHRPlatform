using IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Domain.Entities;
using IntensHRPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IntensHRPlatform.Infrastructure.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly AppDbContext _context;

    public SkillRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task<Skill?> GetByIdAsync(int skillId)
    {
        return await _context.Skills.FindAsync(skillId);
    }

    public async Task<Skill> AddAsync(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Skills
            .AnyAsync(s => s.Name.ToLower() == name.ToLower());
    }
}