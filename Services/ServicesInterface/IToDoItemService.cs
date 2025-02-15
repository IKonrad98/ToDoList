using ToDoApi.Models;

namespace ToDoApi.Services.ServicesInterface;

public interface IToDoItemService
{
    Task<ToDoItemModel> CreateAsync(CreateToDoItemModel model, CancellationToken cancellationToken);

    Task<ToDoItemModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ToDoItemModel> UpdateAsync(UpdateToDoItemModel model, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}