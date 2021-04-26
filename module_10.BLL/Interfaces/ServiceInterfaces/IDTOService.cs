using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace module_10.BLL.Interfaces.ServiceInterfaces
{
    public interface IDTOService<U, V>
    {
        Task<IEnumerable<U>> GetAllAsync();
        Task<U> GetAsync(int? id);
        IEnumerable<U> Find(Func<V, bool> predicate);
        Task CreateAsync(U item);
        Task UpdateAsync(U item);
        Task DeleteAsync(int? id);
    }
}
