using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.Infrastructure.RequestFeatures;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IWordHttpService : IGenericHttpService<WordModel>
    {
        Task<PagingResponse<WordModel>> GetWordsForDictionary(int dictionaryId, PaginationParameters parameters);
        Task<WordInformation> GenerateWordInformation(string wordName);
        Task<ICollection<WordPhoto>> GetWordPictures(string wordName);
    }
}