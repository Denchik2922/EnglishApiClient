namespace EnglishApiClient.Dtos.Entity
{
    public class LearnedWord
    {
        public int Id { get; set; }
        public int CountTrueAnswers { get; set; }
        public bool IsLearned { get; set; }
        public string UserId { get; set; }
        public int WordId { get; set; }
    }
}