using AutoMapper;
using IntensHRPlatform.Domain.Entities;
using IntensHRPlatform.Application.DTOs;

namespace IntensHRPlatform.Application.Mappings;

public class MappingProfile : Profile {

    public MappingProfile()
    {
        CreateMap<Candidate, CandidateResponseDto>()
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src =>
            src.CandidateSkills.Select(cs => cs.Skill)));
        CreateMap<Skill, SkillResponseDto>();
    }
}
