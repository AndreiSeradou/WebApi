using AutoMapper;
using WebApi.Data.Entities;
using WebApi.Models.UserModel;

namespace WebApi.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<UserEntity, User>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();
        }
    }
}
