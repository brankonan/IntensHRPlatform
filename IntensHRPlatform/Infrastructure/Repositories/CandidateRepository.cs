namespace IntensHRPlatform.Infrastructure.Repositories;

using IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Domain.Entities;
using IntensHRPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CandidateRepository : ICandidateRepository  
{
    private readonly AppDbContext _context;

    public CandidateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Candidate>> GetAllAsync()
    {
        return await _context.Candidates
            .Include(c => c.CandidateSkills)
            .ThenInclude(cs => cs.Skill)
            .ToListAsync();
    }

    public async Task<Candidate?> GetByIdAsync (int id)
    {
        return await _context.Candidates
            .Include(c => c.CandidateSkills)
            .ThenInclude(cs => cs.Skill)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Candidate> AddAsync(Candidate candidate)
    {
        _context.Candidates.Add(candidate);
        await _context.SaveChangesAsync();
        return candidate;
    }

    public async Task<Candidate> UpdateAsync(Candidate candidate)
    {
        _context.Candidates.Update(candidate);
        await _context.SaveChangesAsync();
        return candidate;
    }

    public async Task DeleteAsync(int id)
    {
        var candidate = await _context.Candidates.FindAsync(id);
        if (candidate != null)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }

    }
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Candidates
            .AnyAsync(c => c.Email.ToLower() == email.ToLower());
    }

    public async Task<List<Candidate>> SearchAsync(string? name, List<int>? skillIds)
    {
        var query = _context.Candidates
            .Include(c => c.CandidateSkills)
            .ThenInclude(cs => cs.Skill)
            .AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(c => c.FullName.ToLower().Contains(name.ToLower()));

        if (skillIds != null && skillIds.Any())
            query = query.Where(c => c.CandidateSkills.Any(cs => skillIds.Contains(cs.SkillId)));
        
        return await query.ToListAsync();
    }
}
