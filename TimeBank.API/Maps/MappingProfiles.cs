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
            CreateMap<JobCategory, JobCategoryDto>().ReverseMap();
            CreateMap<TokenTransaction, TokenTransactionDto>().ReverseMap();
            CreateMap<UserRegistrationDto, ApplicationUser>()
                .ForMember(u => u.UserName, options => options.MapFrom(x => x.Email))
                .ReverseMap();
        }
    }
}
