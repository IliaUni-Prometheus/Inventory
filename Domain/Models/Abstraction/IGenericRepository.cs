namespace Domain.Models.Abstraction;

public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <param name="whereConditions">The <see cref="object"/></param>
    /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/></returns>
    Task<IEnumerable<TEntity>?> GetAllAsync();

    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id">The <see cref="int"/></param>
    /// <returns>The <see cref="Task{TEntity}"/></returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// InsertAsync
    /// </summary>
    /// <param name="entity">The <see cref="TEntity"/></param>
    /// <returns>The <see cref="Task{int?}"/></returns>
    Task<int?> InsertAsync(TEntity entity);

    /// <summary>
    /// Delete Async
    /// </summary>
    /// <param name="id">The <see cref="int"/></param>
    /// <returns>The <see cref="Task"/></returns>
    Task DeleteAsync(int id);
}