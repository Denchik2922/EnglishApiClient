using Models;

namespace EnglishApiClient.Services.Interfaces
{
    public interface IWordHttpService : IGenericHttpService<WordModel>
    {
        Task<WordInformation> GenerateWordInformation(string wordName);
    }
}