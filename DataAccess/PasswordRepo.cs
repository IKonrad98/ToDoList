using Microsoft.EntityFrameworkCore;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;
using ToDoApi.DataAccess.RepoInterfaces;

namespace ToDoApi.DataAccess;

public class PasswordRepo : GenericRepo<PasswordEntity>, IPasswordRepo
{
    public PasswordRepo(DbContext context)
        : base(context)
    {
    }
}