using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Entity
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(8)]
        public string Description { get; set; }
    }
}
