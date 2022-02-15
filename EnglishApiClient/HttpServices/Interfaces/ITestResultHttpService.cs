using EnglishApiClient.Dtos.Entity;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface ITestResultHttpService
    {
        Task<ICollection<TestResultForStatistic>> GetAllByDictionaryId(int dictionaryId);
    }
}