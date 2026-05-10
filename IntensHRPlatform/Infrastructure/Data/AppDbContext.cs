using IntensHRPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntensHRPlatform.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

public DbSet<Candidate> Candidates { get; set; }
public DbSet<Skill> Skills { get; set; }
public DbSet<CandidateSkill> CandidateSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //relationships
        modelBuilder.Entity<Candidate>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<Skill>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<CandidateSkill>()
            .HasKey(cs => new {cs.CandidateId, cs.SkillId});

        //seed data
        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "C# Programming" },
            new Skill { Id = 2, Name = "Java Programming" },
            new Skill { Id = 3, Name = "Database Design" },
            new Skill { Id = 4, Name = "English" },
            new Skill { Id = 5, Name = "German" }
        );

        modelBuilder.Entity<Candidate>().HasData(
            new Candidate { Id = 1, FullName = "Ana Petrovic", Email = "ana@email.com", DateOfBirth = DateTime.SpecifyKind(new DateTime(1995, 3, 15), DateTimeKind.Utc), ContactNumber = "0641234567" },
            new Candidate { Id = 2, FullName = "Marko Jovic", Email = "marko@email.com", DateOfBirth = DateTime.SpecifyKind(new DateTime(1993, 7, 22), DateTimeKind.Utc), ContactNumber = "0652345678" },
            new Candidate { Id = 3, FullName = "Jovana Ilic", Email = "jovana@email.com", DateOfBirth = DateTime.SpecifyKind(new DateTime(1997, 11, 5), DateTimeKind.Utc), ContactNumber = "0663456789" }
        );

        modelBuilder.Entity<CandidateSkill>().HasData(
            new CandidateSkill { CandidateId = 1, SkillId = 1 },
            new CandidateSkill { CandidateId = 1, SkillId = 4 },
            new CandidateSkill { CandidateId = 2, SkillId = 2 },
            new CandidateSkill { CandidateId = 2, SkillId = 3 },
            new CandidateSkill { CandidateId = 3, SkillId = 1 },
            new CandidateSkill { CandidateId = 3, SkillId = 2 },
            new CandidateSkill { CandidateId = 3, SkillId = 5 }
        );

    }

}
