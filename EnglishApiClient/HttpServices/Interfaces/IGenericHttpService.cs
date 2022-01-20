namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IGenericHttpService<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<bool> Delete(int id);
        Task<ICollection<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Update(T entity);
    }
}