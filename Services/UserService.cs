using AutoMapper;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.RepoInterfaces;
using ToDoApi.Infrastructure;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

namespace ToDoApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepo _repo;
    private readonly IPasswordRepo _passwordRepo;
    private readonly IPasswordEncryptionHelper _passwordEncryptionHelper;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepo repo,
        IPasswordRepo passwordRepo,
        IPasswordEncryptionHelper passwordEncryptionHelper,
        IMapper mapper
        )
    {
        _repo = repo;
        _passwordRepo = passwordRepo;
        _passwordEncryptionHelper = passwordEncryptionHelper;
        _mapper = mapper;
    }

    public async Task<UserModel> CreateAsync(CreateUserModel user, CancellationToken cancellationToken)
    {
        var salt = _passwordEncryptionHelper.GenerateSalt(user.Password);
        var hashedPassword = _passwordEncryptionHelper.HashPassword(user.Password, salt);
        var passwordEntity = new PasswordEntity
        {
            Salt = salt,
            Hash = hashedPassword
        };

        await _passwordRepo.CreateAsync(passwordEntity, cancellationToken);

        var UserEntity = new UserEntity
        {
            UserName = user.UserName,
            Email = user.Email,
            PasswordId = passwordEntity.Id
        };

        var createdUser = await _repo.CreateAsync(UserEntity, cancellationToken);
        var result = _mapper.Map<UserEntity, UserModel>(createdUser);

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
        var user = await _repo.GetByEmailAsync(login.Email, cancellationToken);
        if (user is null)
        {
            throw new Exception("User not found");
        }

        var password = await _passwordRepo.GetByIdAsync(user.PasswordId, cancellationToken);
        var hashedPassword = _passwordEncryptionHelper.VerifyPassword(
            login.Password,
            password.Hash,
            password.Salt
            );

        if (!hashedPassword)
        {
            throw new Exception("Invalid password");
        }

        var result = _mapper.Map<UserEntity, UserModel>(user);

        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var getDeleted = _repo.DeleteAsync(id, cancellationToken);
        await _repo.DeleteAsync(id, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
    }
}