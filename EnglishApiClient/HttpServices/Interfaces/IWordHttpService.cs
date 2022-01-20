using EnglishApiClient.Dtos.Entity;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IWordHttpService : IGenericHttpService<WordModel>
    {
        Task<WordInformation> GenerateWordInformation(string wordName);
        Task<ICollection<WordPhoto>> GetWordPictures(string wordName);
    }
}