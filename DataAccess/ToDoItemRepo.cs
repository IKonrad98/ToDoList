using ToDoApi.Data;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;
using ToDoApi.DataAccess.RepoInterfaces;

namespace ToDoApi.DataAccess;

public class ToDoItemRepo : GenericRepo<ToDoItemEntity>, IToDoItemRepo
{
    public ToDoItemRepo(ToDoApiDbContext context) : base(context)
    {
    }
}