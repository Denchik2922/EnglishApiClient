using EnglishApiClient.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Entity
{
    public class WordModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [EnsureMinimumElements(1, ErrorMessage = "At least one translation is required")]
        public List<TranslatedWord> Translates { get; set; } = new List<TranslatedWord>();

        [Required]
        [EnsureMinimumElements(1, ErrorMessage = "At least one examples is required")]
        public List<ExampleWord> WordExamples { get; set; } = new List<ExampleWord>();

        public int EnglishDictionaryId { get; set; }

        [Required(ErrorMessage = "Transcription is required")]
        public string Transcription { get; set; }
        public string PictureUrl { get; set; }
        public string AudioUrl { get; set; }
    }
}
