using AutoMapper;
using CqrsUser.Domain;
using CqrsUser.DTOs;

namespace CqrsUser.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
