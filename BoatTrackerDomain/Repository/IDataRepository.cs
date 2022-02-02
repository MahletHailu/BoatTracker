using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatTrackerDomain.Repository
{
    public interface IDataRepository<TEntity, TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetAsync(string id);
        Task AddAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        void Delete(TEntity entity);
    }
}
