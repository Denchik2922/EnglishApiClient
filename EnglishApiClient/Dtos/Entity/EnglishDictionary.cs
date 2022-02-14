using EnglishApiClient.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Entity
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
        [EnsureMinimumElements(1, ErrorMessage = "At least one tag is required")]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public bool IsPrivate { get; set; }
    }
}
