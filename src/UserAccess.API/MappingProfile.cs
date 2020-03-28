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
            CreateMap<UserDataModel, User>().ReverseMap();
            CreateMap<User, UserApiModel>().ReverseMap();
        }
    }
}