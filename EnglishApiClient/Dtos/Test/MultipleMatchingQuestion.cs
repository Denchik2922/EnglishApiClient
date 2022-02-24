namespace EnglishApiClient.Dtos.Test
{
    public class MultipleMatchingQuestion
    {
        public TestParameters Parameters { get; set; }
        public ICollection<string> Translates { get; set; }
        public ICollection<string> WordNames { get; set; }
    }
}
