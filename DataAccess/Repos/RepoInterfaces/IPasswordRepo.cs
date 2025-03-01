using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.Repos.GenericRepo;

namespace ToDoApi.DataAccess.Repos.RepoInterfaces;

public interface IPasswordRepo : IGenericRepo<PasswordEntity>
{
}