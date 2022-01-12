using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Interfaces
{
    public interface IWordHttpService
    {
        Task<List<Word>> GetWords();
    }
}