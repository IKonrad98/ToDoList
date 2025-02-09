using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;

namespace ToDoApi.DataAccess.RepoInterfaces;

public interface IUserRepo : IGenericRepo<UserEntity>
{
    Task<UserEntity> GetNameAsync(string login, CancellationToken cancellationToken);

    Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);
}