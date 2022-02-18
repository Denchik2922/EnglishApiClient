﻿using EnglishApiClient.Infrastructure.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace EnglishApiClient.Dtos.Entity
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [EnsureMinimumElements(1, ErrorMessage = "At least one role is required")]
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
