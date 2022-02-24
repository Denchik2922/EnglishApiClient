using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.Dtos.Test;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IBaseTestHttpService<T> where T : class
    {
        Task<ParamsForCheck> CheckQuestion(ParamsFoAnswer parameters);
        Task<bool> FinishTest(TestResult result);
        Task<T> GetPartOfTest(TestParameters parameters);
        Task<TestParameters> StartTest(int Dictionaryid);
    }
}