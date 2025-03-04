using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.Repos.GenericRepo;
using ToDoApi.DataAccess.Repos.RepoInterfaces;

namespace ToDoApi.DataAccess.Repos;

public class TokenRepo : GenericRepo<TokenEntity>, ITokenRepo
{
    public TokenRepo(ToDoApiDbContext context)
        : base(context)
    {
    }
}