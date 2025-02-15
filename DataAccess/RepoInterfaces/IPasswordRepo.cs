using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;

namespace ToDoApi.DataAccess.RepoInterfaces;

public interface IPasswordRepo : IGenericRepo<PasswordEntity>
{
}