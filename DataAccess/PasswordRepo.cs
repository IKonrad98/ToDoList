using ToDoApi.Data;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;
using ToDoApi.DataAccess.RepoInterfaces;

namespace ToDoApi.DataAccess;

public class PasswordRepo : GenericRepo<PasswordEntity>, IPasswordRepo
{
    public PasswordRepo(ToDoApiDbContext context)
        : base(context)
    {
    }
}