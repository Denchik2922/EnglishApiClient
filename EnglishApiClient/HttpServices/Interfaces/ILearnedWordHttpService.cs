using EnglishApiClient.Dtos.Entity;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface ILearnedWordHttpService
    {
        Task<ICollection<LearnedWord>> GetAllForUser(int dictionaryId);
        Task<ICollection<LearnedWord>> GetAllByDictionaryId(int dictionaryId);
        Task<bool> SetLearned(LearnedWord word);
    }
}