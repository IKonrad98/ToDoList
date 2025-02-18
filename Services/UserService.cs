using Microsoft.AspNetCore.JsonPatch;
using ToDoApi.Data.Entities;
using ToDoApi.DataAccess.RepoInterfaces;
using ToDoApi.Infrastructure;
using ToDoApi.Models;
using ToDoApi.Services.ServicesInterface;

public class UserService : IUserService
{
    private readonly IUserRepo _repo;
    private readonly IPasswordRepo _passwordRepo;
    private readonly IPasswordEncryptionHelper _passwordEncryptionHelper;

    public UserService(
        IUserRepo repo,
        IPasswordRepo passwordRepo,
        IPasswordEncryptionHelper passwordEncryptionHelper
    )
    {
        _repo = repo;
        _passwordRepo = passwordRepo;
        _passwordEncryptionHelper = passwordEncryptionHelper;
    }

    public async Task<UserModel> CreateAsync(CreateUserModel user, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.UserName))
        {
            throw new ArgumentException("Invalid user data");
        }
        try
        {
            var existingUser = await _repo.GetByEmailAsync(user.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            var salt = _passwordEncryptionHelper.GenerateSalt(user.Password);
            var hashedPassword = _passwordEncryptionHelper.HashPassword(user.Password, salt);
            var passwordEntity = new PasswordEntity
            {
                Salt = salt,
                Hash = hashedPassword
            };
            await _passwordRepo.CreateAsync(passwordEntity, cancellationToken);

            var userEntity = new UserEntity
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordId = passwordEntity.Id,
                CreateUser = DateTime.UtcNow
            };

            var createdUser = await _repo.CreateAsync(userEntity, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);

            return new UserModel
            {
                Id = createdUser.Id,
                UserName = createdUser.UserName,
                Email = createdUser.Email,
                CreateUser = (DateTime)createdUser.CreateUser
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error creating user", ex);
        }
    }

    public async Task<UserModel> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByEmailAsync(email, cancellationToken);

        if (entity is null)
        {
            throw new Exception("User not found");
        }

        return new UserModel
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email
        };
    }

    public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            throw new Exception("User not found");
        }

        return new UserModel
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email
        };
    }

    public async Task<List<ToDoItemModel>> GetAllToDo(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetAllToDo(id, cancellationToken);
        if (entity is null)
        {
            throw new Exception("Not found");
        }
        return entity.ToDoItems.Select(x => new ToDoItemModel
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            CreateItem = x.CreateItem,
            IsCompleted = x.IsCompleted,
            UserId = x.UserId,
            Deadline = x.Deadline,
            Priority = x.Priority ?? PriorityLevel.Low
        }).ToList();
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

        return new UserModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }

    public async Task<UserModel> UpdateAsync(
        Guid id,
        JsonPatchDocument<UpdateUserModel> model,
        CancellationToken cancellationToken
        )
    {
        var user = await _repo.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        var userToUpdate = new UpdateUserModel
        {
            UserName = user.UserName,
            Email = user.Email
        };

        model.ApplyTo(userToUpdate);

        user.UserName = userToUpdate.UserName ?? user.UserName;
        user.Email = userToUpdate.Email ?? user.Email;

        await _repo.UpdateAsync(user, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);

        var result = new UserModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };

        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(id, cancellationToken);
    }
}