namespace EnglishApiClient.Infrastructure.RequestFeatures
{
    public class SearchParameters
    {
        public string SearchTerm { get; set; } 
        public ICollection<string> SearchTags { get; set; } = new List<string>();
    }
}
