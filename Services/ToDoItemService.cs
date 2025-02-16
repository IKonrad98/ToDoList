using Microsoft.AspNetCore.JsonPatch;
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
            IsCompleted = false,
            CreateItem = DateTime.UtcNow,
            Deadline = null,
            Priority = model.Priority ?? PriorityLevel.Low,
            UserId = model.UserId,
        };

        var addedEntity = await _repo.CreateAsync(entity, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);

        var result = new ToDoItemModel
        {
            Id = addedEntity.Id,
            Title = addedEntity.Title,
            Description = addedEntity.Description,
            IsCompleted = addedEntity.IsCompleted,
            CreateItem = addedEntity.CreateItem,
            Deadline = addedEntity.Deadline,
            Priority = addedEntity.Priority ?? PriorityLevel.Low,
            UserId = addedEntity.UserId
        };

        return result;
    }

    public async Task<ToDoItemModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

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

    public async Task<ToDoItemModel> UpdateAsync(
        JsonPatchDocument<UpdateToDoItemModel> model,
        Guid id,
        CancellationToken cancellationToken
        )
    {
        var entity = await _repo.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        var updateModel = new UpdateToDoItemModel
        {
            Title = entity.Title,
            Description = entity.Description,
            IsCompleted = entity.IsCompleted,
            Deadline = entity.Deadline,
            Priority = entity.Priority
        };

        model.ApplyTo(updateModel);

        entity.Title = updateModel.Title ?? entity.Title;
        entity.Description = updateModel.Description ?? entity.Description;
        entity.IsCompleted = updateModel.IsCompleted ?? entity.IsCompleted;
        entity.Deadline = updateModel.Deadline ?? entity.Deadline;
        entity.Priority = updateModel.Priority ?? entity.Priority;

        var updatedEntity = await _repo.UpdateAsync(entity, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);

        var result = new ToDoItemModel
        {
            Id = updatedEntity.Id,
            Title = updatedEntity.Title,
            Description = updatedEntity.Description,
            CreateItem = updatedEntity.CreateItem,
            IsCompleted = updatedEntity.IsCompleted,
            UserId = updatedEntity.UserId,
            Priority = updatedEntity.Priority ?? PriorityLevel.Low,
            Deadline = updatedEntity.Deadline
        };

        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(id, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
    }
}