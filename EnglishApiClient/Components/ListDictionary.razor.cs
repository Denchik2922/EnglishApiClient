using EnglishApiClient.Dtos.Entity;
using Microsoft.AspNetCore.Components;

namespace EnglishApiClient.Components
{
    public partial class ListDictionary
    {

        [Parameter]
        public ICollection<EnglishDictionary> Dictionaries { get; set; }
    }
}
