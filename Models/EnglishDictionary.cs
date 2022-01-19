using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class EnglishDictionary
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(4)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(8)]
        public string Description { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Tags is required")]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<WordModel> Words { get; set; } = new List<WordModel>();
        public ICollection<TestUserResult> SpellingTestResults { get; set; } = new List<TestUserResult>();
        public ICollection<TestUserResult> MatchingTestResults { get; set; } = new List<TestUserResult>();
        public bool IsPrivate { get; set; }
    }
}
