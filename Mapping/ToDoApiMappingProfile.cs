using AutoMapper;
using ToDoApi.Data.Entities;
using ToDoApi.Models;

public class ToDoApiMappingProfile : Profile
{
    public ToDoApiMappingProfile()
    {
        CreateMap<ToDoItemModel, ToDoItemEntity>().ReverseMap();
        CreateMap<CreateToDoItemModel, ToDoItemEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.CreateItem, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UpdateToDoItemModel, ToDoItemEntity>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<UserEntity, UserModel>().ReverseMap();

        CreateMap<CreateUserModel, UserEntity>()
            .ForMember(dest => dest.PasswordId, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.CreateUser, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ToDoItems, opt => opt.Ignore());

        CreateMap<LoginUserModel, UserEntity>().ReverseMap();
    }
}