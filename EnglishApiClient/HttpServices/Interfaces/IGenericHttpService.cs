using EnglishApiClient.Infrastructure.RequestFeatures;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IGenericHttpService<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<bool> Delete(int id);
        Task<PagingResponse<T>> GetAll(PaginationParameters parameters);
        Task<T> GetById(int id);
        Task<bool> Update(T entity);
    }
}