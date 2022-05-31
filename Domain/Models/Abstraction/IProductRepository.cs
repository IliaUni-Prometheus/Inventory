using Infrastructure.Models;

namespace Domain.Models.Abstraction;

public interface IProductRepository
{
    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <param name="whereConditions">The <see cref="object"/></param>
    /// <returns>The <see cref="Task{IEnumerable{TEntity}}"/></returns>
    Task<IEnumerable<Product>> GetAllAsync();

    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id">The <see cref="int"/></param>
    /// <returns>The <see cref="Task{TEntity}"/></returns>
    Task<Product?> GetByIdAsync(int id);

    /// <summary>
    /// InsertAsync
    /// </summary>
    /// <param name="product"></param>
    /// <param name="entity">The <see cref="product"/></param>
    /// <returns>The <see cref="Task{int?}"/></returns>
    Task<int> InsertAsync(Product product);

    /// <summary>
    /// Delete Async
    /// </summary>
    /// <param name="id">The <see cref="int"/></param>
    /// <returns>The <see cref="Task"/></returns>
    Task DeleteAsync(int id);
}