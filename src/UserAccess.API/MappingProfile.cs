using System.Diagnostics.CodeAnalysis;
using UserAccess.API.Models;
using UserAccess.Data.Mongo.Models;
using UserAccess.Models;
using AutoMapper;

namespace UserAccess.API
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserApiModel, UserDataModel>();
            CreateMap<User, UserApiModel>().ReverseMap();
            CreateMap<UserUpdateApiModel, User>().ReverseMap();
            CreateMap<UserDataModel, User>().ReverseMap();
        }
    }
}