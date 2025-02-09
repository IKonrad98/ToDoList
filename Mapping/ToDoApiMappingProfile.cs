using AutoMapper;
using ToDoApi.Data.Entities;
using ToDoApi.Models;

namespace ToDoApi.Mappings;

public class ToDoApiMappingProfile : Profile
{
    public ToDoApiMappingProfile()
    {
        CreateMap<ToDoItemEntity, ToDoItemModel>().ReverseMap();
        CreateMap<CreateToDoItemModel, ToDoItemEntity>().ReverseMap();
        CreateMap<UpdateToDoItemModel, ToDoItemEntity>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UserEntity, UserModel>().ReverseMap();
        CreateMap<RegisterUserModel, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}