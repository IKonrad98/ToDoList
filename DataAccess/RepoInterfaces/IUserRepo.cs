using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;

namespace ToDoApi.DataAccess.RepoInterfaces;

public interface IUserRepo : IGenericRepo<UserEntity>
{
    Task<UserEntity> GetLoginAsync(string login, CancellationToken cancellationToken);

    Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);
}