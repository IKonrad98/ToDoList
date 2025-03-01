using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.Repos.GenericRepo;
using ToDoApi.DataAccess.Repos.RepoInterfaces;

namespace ToDoApi.DataAccess.Repos;

public class ToDoItemRepo : GenericRepo<ToDoItemEntity>, IToDoItemRepo
{
    public ToDoItemRepo(ToDoApiDbContext context) : base(context)
    {
    }
}