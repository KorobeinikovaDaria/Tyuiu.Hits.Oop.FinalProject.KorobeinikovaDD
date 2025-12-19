using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Components.Pages;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public int Level { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Tests> Tests { get; set; }
    }
}
