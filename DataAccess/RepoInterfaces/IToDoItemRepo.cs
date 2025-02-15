using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;

namespace ToDoApi.DataAccess.RepoInterfaces;

public interface IToDoItemRepo : IGenericRepo<ToDoItemEntity>
{
}