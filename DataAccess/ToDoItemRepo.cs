using Microsoft.EntityFrameworkCore;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.GenericRepo;
using ToDoApi.DataAccess.RepoInterfaces;

namespace ToDoApi.DataAccess;

public class ToDoItemRepo : GenericRepo<ToDoItemEntity>, IToDoItemRepo
{
    public ToDoItemRepo(DbContext context) : base(context)
    {
    }
}