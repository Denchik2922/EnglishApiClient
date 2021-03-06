using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.Infrastructure.RequestFeatures;
using Tewr.Blazor.FileReader;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IWordHttpService : IGenericHttpService<WordModel>
    {
        Task<PagingResponse<WordModel>> GetWordsForDictionary(int dictionaryId, PaginationParameters parameters);
        Task<WordInformation> GenerateWordInformation(string wordName);
        Task<ICollection<WordPhoto>> GetWordPictures(string wordName);
        Task<string> UploadWordImage(IFileReference file);
    }
}