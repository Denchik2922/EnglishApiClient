using System.Collections.Generic;

namespace Models
{
    public class EnglishDictionary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Word> Words { get; set; } = new List<Word>();
        public ICollection<TestUserResult> SpellingTestResults { get; set; } = new List<TestUserResult>();
        public ICollection<TestUserResult> MatchingTestResults { get; set; } = new List<TestUserResult>();
        public bool IsPrivate { get; set; }
    }
}
