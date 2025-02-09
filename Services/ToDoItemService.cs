using AutoMapper;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.RepoInterfaces;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace ToDoApi.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IToDoItemRepo _repo;
    private readonly IMapper _mapper;

    public ToDoItemService(IToDoItemRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ToDoItemModel> CreateAsync(CreateToDoItemModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateToDoItemModel, ToDoItemEntity>(model);
        var addedEntity = await _repo.CreateAsync(entity, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<ToDoItemEntity, ToDoItemModel>(addedEntity);

        return result;
    }

    public async Task<ToDoItemModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(id, cancellationToken);
        var result = _mapper.Map<ToDoItemEntity, ToDoItemModel>(entity);

        return result;
    }

    public async Task<ToDoItemModel> UpdateAsync(ToDoItemModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ToDoItemModel, ToDoItemEntity>(model);
        var updatedEntity = await _repo.UpdateAsync(entity, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<ToDoItemEntity, ToDoItemModel>(updatedEntity);

        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var getDeleted = _repo.DeleteAsync(id, cancellationToken);
        await _repo.DeleteAsync(id, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
    }
}