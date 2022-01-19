using System.Collections.Generic;

namespace Models
{
    public class WordModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TranslatedWord> Translates { get; set; } = new List<TranslatedWord>();
        public int EnglishDictionaryId { get; set; }
        public string Transcription { get; set; }
        public string Picture { get; set; }
        public string Audio { get; set; }
    }
}
