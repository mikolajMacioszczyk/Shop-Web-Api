using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.DAL.Repositories
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetQuerable();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}