using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApiClient.Services.Interfaces
{
    public interface IWordHttpService
    {
        Task<List<Word>> GetWords();
    }
}