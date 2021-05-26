﻿using System.ComponentModel.DataAnnotations;

namespace EmployerWebApp.Models
{
    public class DescriptionSource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 50)]
        public string Text { get; set; }

        public int WordsCount
            => Text.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
