using Domain.Models.Abstraction.Base;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly NorthwindContext _db;

        public RepositoryBase(NorthwindContext db)
        {
            this._db = db;
        }

        public Task<T?> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool?> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> RetrieveAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> RetrieveByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
