using EnglishApiClient.Dtos.Interfaces;

namespace EnglishApiClient.Dtos.Entity
{
    public class ExampleWord : IExtraWordInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
