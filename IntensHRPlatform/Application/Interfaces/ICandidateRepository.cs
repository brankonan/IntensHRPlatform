namespace IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Domain.Entities;

public interface ICandidateRepository {

    Task<List<Candidate>> GetAllAsync();

    Task<Candidate?> GetByIdAsync(int id);

    Task<Candidate> AddAsync(Candidate candidate);

    Task<Candidate> UpdateAsync(Candidate candidate);
    
    Task DeleteAsync(int id);

    Task<bool> ExistsByEmailAsync(string email);

    Task<List<Candidate>> SearchAsync(string? name, List<int>? skillIds);

}
