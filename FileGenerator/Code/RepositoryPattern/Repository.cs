using System.Collections.Generic;
using System.Threading.Tasks;

// update YOUR_NAMESPACE with your namespace
namespace YOUR_NAMESPACE
{
    public class Repository<T>: IRepository<T> where T : class
    {
        // update YOUR_CONTEXT with your context class
        private readonly YOUR_CONTEXT _context;
        public Repository(YOUR_CONTEXT context)
        {
            _context = context;
        }
        public async Task<T> GetAsync(int id)
        {
            // Implement your logic here
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Implement your logic here
        }
        public async Task AddAsync(T entity)
        {
            // Implement your logic here
        }
        public async Task UpdateAsync(T entity)
        {
            // Implement your logic here
        }
        public async Task DeleteAsync(T entity)
        {
            // Implement your logic here
        }
    }
}
