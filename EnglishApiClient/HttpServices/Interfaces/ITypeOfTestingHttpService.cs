using EnglishApiClient.Dtos.Entity;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface ITypeOfTestingHttpService : IGenericHttpService<TypeOfTesting>
    {
        Task<ICollection<TypeOfTesting>> GetAllWithoutPage();
    }
}
