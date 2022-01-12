using System.Collections.Generic;

namespace Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TranslatedWord> Translates { get; set; }
        public int EnglishDictionaryId { get; set; }
        public string Transcription { get; set; }
        public string Picture { get; set; }
        public string Audio { get; set; }
    }
}
