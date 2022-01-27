using EnglishApiClient.Dtos.Entity;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface ITagHttpService : IGenericHttpService<Tag>
    {
        Task<ICollection<Tag>> GetAllWithoutPage();
    }
}
