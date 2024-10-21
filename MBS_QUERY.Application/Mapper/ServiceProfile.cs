using AutoMapper;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Domain.Documents;
using MBS_QUERY.Domain.Entities;
using Response = MBS_QUERY.Contract.Services.Mentors.Response;
using SkillResponse = MBS_QUERY.Contract.Services.Skills.Response;
namespace MBS_QUERY.Application.Mapper;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // ==================SkillMapping=====================
        CreateMap<Skill, SkillResponse.GetSkillsQuery>().ReverseMap();
        
        CreateMap<PagedResult<Skill>, PagedResult<SkillResponse.GetSkillsQuery>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        
        // ==================MentorMapping=====================
        CreateMap<MentorProjection, Response.GetMentorResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DocumentId))
            .ConstructUsing((src, context) => new Response.GetMentorResponse()
            {
                Email = src.Email,
                Point = src.Points,
                FullName = src.FullName,
                CreatedOnUtc = src.CreatedOnUtc,
                Skills = src.MentorSkills == null ? [] : src.MentorSkills.Select(s => new Response.Skill()
                {
                    SkillName = s.Name,
                    SkillDesciption = s.Description,
                    CreatedOnUtc = s.CreatedOnUtc,
                    SkillCategoryType = s.CateogoryType,
                    Cetificates = s.SkillCetificates.Select(c => new Response.Cetificate()
                    {
                        CetificateName = c.Name,
                        CetificateImageUrl = c.ImageUrl,
                        CetificateDesciption = c.Description,
                        CreatedOnUtc = c.CreatedOnUtc
                    }).ToList()
                }).ToList(),
            });
        
        CreateMap<MentorProjection, Response.GetAllMentorsResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DocumentId))
            .ConstructUsing((src, context) => new Response.GetAllMentorsResponse()
            {
                Email = src.Email,
                Point = src.Points,
                FullName = src.FullName,
                CreatedOnUtc = src.CreatedOnUtc,
                Skills = src.MentorSkills == null ? [] : src.MentorSkills.Select(s => s.Name).ToList()
            });
        
        CreateMap<PagedResult<MentorProjection>, PagedResult<Response.GetAllMentorsResponse>>()
            .ConstructUsing((src, context) =>
            {
                var mappedItems = src.Items.Select(item => context.Mapper.Map<Response.GetAllMentorsResponse>(item)).ToList();
                return new PagedResult<Response.GetAllMentorsResponse>(mappedItems, src.PageIndex, src.PageSize, src.TotalCount);
            });

        CreateMap<Subject, Contract.Services.Subjects.Response.GetSubjectsQuery>().ReverseMap();
        CreateMap<PagedResult<Subject>, PagedResult<Contract.Services.Subjects.Response.GetSubjectsQuery>>()
            .ForMember(dest=>dest.Items, opt=>opt.MapFrom(src=>src.Items));
    }
    
}
