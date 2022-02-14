namespace EnglishApiClient.Dtos.Entity
{
    public class TestResult
    {
        public int Id { get; set; }
        public int EnglishDictionaryId { get; set; }
        public string UserId { get; set; }
        public double Score { get; set; }
        public int TypeOfTestingId { get; set; }
        public DateTime Date { get; set; }
    }
}
