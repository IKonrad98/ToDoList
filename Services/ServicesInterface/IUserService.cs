using Microsoft.AspNetCore.JsonPatch;
using ToDoApi.Models;

namespace ToDoApi.Services.ServicesInterface;

public interface IUserService
{
    Task<UserModel> CreateAsync(CreateUserModel user, CancellationToken cancellationToken);

    Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<ToDoItemModel>> GetAllToDo(Guid id, CancellationToken cancellationToken);

    Task<UserModel> LoginAsync(LoginUserModel login, CancellationToken cancellationToken);

    Task<UserModel> UpdateAsync(Guid id, JsonPatchDocument<UpdateUserModel> model, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}