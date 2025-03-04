using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.Repos.GenericRepo;

namespace ToDoApi.DataAccess.Repos.RepoInterfaces;

public interface IUserRepo : IGenericRepo<UserEntity>
{
    Task<UserEntity> GetNameAsync(string login, CancellationToken cancellationToken);

    Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserEntity> GetAllToDo(Guid id, CancellationToken cancellationToken);
}