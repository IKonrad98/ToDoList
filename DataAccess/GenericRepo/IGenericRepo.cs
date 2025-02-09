namespace ToDoApi.DataAccess.GenericRepo;

public interface IGenericRepo<T> where T : class
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

    Task<T> GetByIdAsync(Guid? id, CancellationToken cancellationToken);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);

    Task DeleteAsync(Guid? id, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}