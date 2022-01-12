using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Interfaces
{
    public interface IGenericHttpService<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<bool> Delete(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Update(T entity);
    }
}