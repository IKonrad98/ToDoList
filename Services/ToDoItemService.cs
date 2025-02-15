using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.RepoInterfaces;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace ToDoApi.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IToDoItemRepo _repo;

    public ToDoItemService(IToDoItemRepo repo)
    {
        _repo = repo;
    }

    public async Task<ToDoItemModel> CreateAsync(CreateToDoItemModel model, CancellationToken cancellationToken)
    {
        var entity = new ToDoItemEntity
        {
            Title = model.Title,
            Description = model.Description,
            CreateItem = DateTime.UtcNow,
            IsCompleted = false,
            UserId = model.UserId,
            Priority = PriorityLevel.Low,
        };

        var addedEntity = await _repo.CreateAsync(entity, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);

        var result = new ToDoItemModel
        {
            Id = addedEntity.Id,
            Title = addedEntity.Title,
            Description = addedEntity.Description,
            CreateItem = addedEntity.CreateItem,
            IsCompleted = addedEntity.IsCompleted,
            UserId = addedEntity.UserId
        };

        return result;
    }

    public async Task<ToDoItemModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(id, cancellationToken);

        var result = new ToDoItemModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            CreateItem = entity.CreateItem,
            IsCompleted = entity.IsCompleted,
            UserId = entity.UserId
        };

        return result;
    }

    public async Task<ToDoItemModel> UpdateAsync(ToDoItemModel model, CancellationToken cancellationToken)
    {
        var entity = new ToDoItemEntity
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            CreateItem = model.CreateItem,
            IsCompleted = model.IsCompleted,
            UserId = model.UserId
        };

        var updatedEntity = await _repo.UpdateAsync(entity, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);

        var result = new ToDoItemModel
        {
            Id = updatedEntity.Id,
            Title = updatedEntity.Title,
            Description = updatedEntity.Description,
            CreateItem = updatedEntity.CreateItem,
            IsCompleted = updatedEntity.IsCompleted,
            UserId = updatedEntity.UserId
        };

        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(id, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
    }
}