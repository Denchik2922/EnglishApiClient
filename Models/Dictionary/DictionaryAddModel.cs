using System.Collections.Generic;

namespace Models.Dictionary
{
    public class DictionaryAddModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public bool IsPrivate { get; set; } = true;
    }
}
