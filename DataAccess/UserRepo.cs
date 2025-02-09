using Microsoft.EntityFrameworkCore;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;
using ToDoApi.DataAccess.RepoInterfaces;

namespace ToDoApi.DataAccess;

public class UserRepo : GenericRepo<UserEntity>, IUserRepo
{
    public UserRepo(DbContext context)
        : base(context)
    {
    }

    public async Task<UserEntity> GetNameAsync(string login, CancellationToken cancellationToken)
    {
        var entity = await _dbSet
            .FirstOrDefaultAsync(u => u.UserName == login, cancellationToken);

        return entity;
    }

    public async Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var entity = await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        return entity;
    }
}