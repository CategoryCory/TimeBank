using AutoMapper;
using TimeBank.API.Dtos;
using TimeBank.Entities.Models;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.API.Maps
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<UserRegistrationDto, ApplicationUser>()
                .ForMember(u => u.UserName, options => options.MapFrom(x => x.Email))
                .ReverseMap();
        }
    }
}
