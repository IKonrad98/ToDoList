using Microsoft.AspNetCore.JsonPatch;
using ToDoApi.Models;

namespace ToDoApi.Services.ServicesInterface;

public interface IToDoItemService
{
    Task<ToDoItemModel> CreateAsync(CreateToDoItemModel model, CancellationToken cancellationToken);

    Task<ToDoItemModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ToDoItemModel> UpdateAsync(
        JsonPatchDocument<UpdateToDoItemModel> model,
        Guid id,
        CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}