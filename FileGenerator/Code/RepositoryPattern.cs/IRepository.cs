using System.Collections.Generic;
using System.Threading.Tasks;

// update YOUR_NAMESPACE with your namespace
namespace YOUR_NAMESPACE 
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
