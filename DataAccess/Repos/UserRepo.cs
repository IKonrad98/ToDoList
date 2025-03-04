using Microsoft.EntityFrameworkCore;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.Repos.GenericRepo;
using ToDoApi.DataAccess.Repos.RepoInterfaces;

namespace ToDoApi.DataAccess.Repos;

public class UserRepo : GenericRepo<UserEntity>, IUserRepo
{
    public UserRepo(ToDoApiDbContext context)
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

    public async Task<UserEntity> GetAllToDo(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet
            .Include(u => u.ToDoItems)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        return entity;
    }
}