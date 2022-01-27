using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.Infrastructure.RequestFeatures;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IDictionaryHttpService : IGenericHttpService<EnglishDictionary>
    {
        Task<PagingResponse<EnglishDictionary>> GetPublicDictionaries(PaginationParameters parameters);
        Task<PagingResponse<EnglishDictionary>> GetPrivateDictionaries(PaginationParameters parameters);
    }
}