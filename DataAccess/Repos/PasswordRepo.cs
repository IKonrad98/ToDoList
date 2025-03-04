using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.Repos.GenericRepo;
using ToDoApi.DataAccess.Repos.RepoInterfaces;

namespace ToDoApi.DataAccess.Repos;

public class PasswordRepo : GenericRepo<PasswordEntity>, IPasswordRepo
{
    public PasswordRepo(ToDoApiDbContext context)
        : base(context)
    {
    }
}