using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Interfaces
{
    public interface IDictionaryHttpService : IGenericHttpService<EnglishDictionary>
    {
        Task<ICollection<EnglishDictionary>> GetPrivateDictionaries();
        Task<ICollection<EnglishDictionary>> GetPublicDictionaries();
    }
}