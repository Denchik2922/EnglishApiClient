using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.Infrastructure.RequestFeatures;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IUserHttpService
    {
        Task<PagingResponse<User>> GetAll(PaginationParameters parameters);
    }
}