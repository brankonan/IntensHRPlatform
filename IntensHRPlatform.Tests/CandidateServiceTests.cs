using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IntensHRPlatform.Application.DTOs;
using IntensHRPlatform.Application.Interfaces;
using IntensHRPlatform.Application.Services;
using IntensHRPlatform.Domain.Entities;
using Moq;

namespace IntensHRPlatform.Tests
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly Mock<ISkillRepository> _skillRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CandidateService _service;

        public CandidateServiceTests()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _skillRepositoryMock = new Mock<ISkillRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new CandidateService(
                _candidateRepositoryMock.Object,
                _skillRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ThrowsException_WhenEmailExists()
        {
            _candidateRepositoryMock
                .Setup(r => r.ExistsByEmailAsync("ana@email.com"))
                .ReturnsAsync(true);

            var dto = new CreateCandidateDto { Email = "ana@email.com" };

            await Assert.ThrowsAsync<Exception>(() =>
            _service.CreateAsync(dto));
        }

        [Fact]
        public async Task AddSkillToCandidate_ThrowsException_WhenSkillAlreadyAssigned()
        {
            var candidate = new Candidate
            {
                Id = 1,
                CandidateSkills = new List<CandidateSkill> { new
                CandidateSkill { SkillId = 1 } }
            };
            _candidateRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(candidate);

            _skillRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Skill { Id = 1, Name = "C#" });

            await Assert.ThrowsAsync<Exception>(() =>
            _service.AddSkillToCandidateAsync(1, 1));


        }

        [Fact]
        public async Task RemoveSkillFromCandidate_ThrowsException_WhenSkillNotAssigned()
        {
            var candidate = new Candidate
            {
                Id = 1,
                CandidateSkills = new List<CandidateSkill>()
            };

            _candidateRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(candidate);

            await Assert.ThrowsAsync<Exception>(() => _service.RemoveSkillFromCandidateAsync(1, 99));
        }

        [Fact]
        public async Task DeleteAsync_ThrowsException_WhenCandidateNotFound()
        {
            _candidateRepositoryMock
                .Setup(r => r.GetByIdAsync(99))
                .ReturnsAsync((Candidate?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(99));
        }
    }

    
}
