using AutoMapper;
using TimeBank.API.Dtos;
using TimeBank.Repository.IdentityModels;
using TimeBank.Repository.Models;

namespace TimeBank.API.Maps
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<Job, JobResponseDto>()
                .ForMember(dest => dest.JobCategory, opt => opt.MapFrom(src => src.JobCategory.JobCategoryName))
                .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedBy.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.FullName));
            CreateMap<JobSchedule, JobScheduleDto>().ReverseMap();
            CreateMap<JobApplication, JobApplicationResponseDto>()
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.Job.JobName))
                .ForMember(dest => dest.JobCreatedByName, opt => opt.MapFrom(src => src.Job.CreatedBy.FullName))
                .ForMember(dest => dest.ApplicantName, opt => opt.MapFrom(src => src.Applicant.FullName));
            CreateMap<JobCategory, JobCategoryDto>().ReverseMap();
            CreateMap<TokenTransaction, TokenTransactionDto>().ReverseMap();
            CreateMap<UserRegistrationDto, ApplicationUser>()
                .ForMember(u => u.UserName, options => options.MapFrom(x => x.Email))
                .ReverseMap();
            CreateMap<ApplicationUser, UserProfileResponseDto>();
            CreateMap<UserProfileUpdateDto, ApplicationUser>()
                .ForMember(u => u.Skills, act => act.Ignore());
            CreateMap<UserRatingsDto, UserRating>();
            CreateMap<UserRating, UserRatingsResponseDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName))
                .ForMember(dest => dest.RevieweeName, opt => opt.MapFrom(src => src.Reviewee.FullName));
            CreateMap<UserSkill, UserSkillsDto>().ReverseMap();
        }
    }
}
