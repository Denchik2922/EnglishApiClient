using EnglishApiClient.Dtos.Entity;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IDictionaryHttpService : IGenericHttpService<EnglishDictionary>
    {
        Task<ICollection<EnglishDictionary>> GetPrivateDictionaries();
        Task<ICollection<EnglishDictionary>> GetPublicDictionaries();
    }
}