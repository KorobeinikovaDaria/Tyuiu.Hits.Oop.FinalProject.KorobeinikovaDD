using System.ComponentModel.DataAnnotations;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Content { get; set; }

        public int CourseId { get; set; } 
        public Course? Course { get; set; } 
    }
}
