namespace Domain.Models.Abstraction.Base
{
    public interface IRepositoryBase<T>
    {
        Task<T?> CreateAsync(T entity);
        Task<IEnumerable<T>> RetrieveAllAsync();
        Task<T?> RetrieveByIdAsync(int id);
        Task<bool> UpdateAsync(T entity);
        Task<bool?> DeleteByIdAsync(int id);
    }
}
