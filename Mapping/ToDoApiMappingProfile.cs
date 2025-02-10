using AutoMapper;
using ToDoApi.Data.Entities;
using ToDoApi.Models;

public class ToDoApiMappingProfile : Profile
{
    public ToDoApiMappingProfile()
    {
        CreateMap<ToDoItemModel, ToDoItemEntity>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<CreateToDoItemModel, ToDoItemEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.CreateItem, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        CreateMap<UpdateToDoItemModel, ToDoItemEntity>()
            .ForMember(dest => dest.CreateItem, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}