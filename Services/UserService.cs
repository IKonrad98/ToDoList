using AutoMapper;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.RepoInterfaces;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace ToDoApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepo _repo;
    private readonly IMapper _mapper;

    public UserService(IUserRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<UserModel> CreateAsync(CreateUserModel user, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateUserModel, UserEntity>(user);
        var addedEntity = await _repo.CreateAsync(entity, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<UserEntity, UserModel>(addedEntity);
        return result;
    }

    public async Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByEmailAsync(email, cancellationToken);
        var result = _mapper.Map<UserEntity, UserModel>(entity);
        return result;
    }

    public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(id, cancellationToken);
        var result = _mapper.Map<UserEntity, UserModel>(entity);
        return result;
    }

    public async Task<UserModel> LoginAsync(LoginUserModel login, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<LoginUserModel, UserEntity>(login);
        var result = await _repo.LoginAsync(entity, cancellationToken);
        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var getDeleted = _repo.DeleteAsync(id, cancellationToken);
        await _repo.DeleteAsync(id, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
    }
}