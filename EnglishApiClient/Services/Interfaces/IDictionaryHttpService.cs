using Models;
using Models.Dictionary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Services.Interfaces
{
    public interface IDictionaryHttpService : IGenericHttpService<EnglishDictionary>
    {
        Task<ICollection<EnglishDictionary>> GetPrivateDictionaries();
        Task<ICollection<EnglishDictionary>> GetPublicDictionaries();
    }
}