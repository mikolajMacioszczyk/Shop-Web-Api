using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> CreateAsync(T created);
        Task<bool> UpdateAsync(int id, T updated);
        Task<bool> RemoveAsync(int id);
        Task SaveChangesAsync();
    }
}