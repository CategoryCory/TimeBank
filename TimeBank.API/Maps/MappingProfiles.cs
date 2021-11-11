using AutoMapper;
using TimeBank.API.Dtos;
using TimeBank.Entities.Models;

namespace TimeBank.API.Maps
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
