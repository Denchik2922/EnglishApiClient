using EnglishApiClient.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Test
{
    public class SpellingAnswerModel
    {
        [OnlyLetters(ErrorMessage = "Enter only Alphabets")]
        public string AnswerForQuestion { get; set; } = "";
    }
}
