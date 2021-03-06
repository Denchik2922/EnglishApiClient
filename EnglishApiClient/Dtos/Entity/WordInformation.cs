namespace EnglishApiClient.Dtos.Entity
{
    public class WordInformation
    {
        public string Translate { get; set; }
        public string Transcription { get; set; }
        public ICollection<WordPhoto> PictureUrls { get; set; }
        public string AudioUrl { get; set; }
    }
}
