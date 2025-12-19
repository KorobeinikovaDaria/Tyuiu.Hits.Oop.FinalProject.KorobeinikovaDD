using System.ComponentModel.DataAnnotations;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models
{
    public class Tests
    {
        public int Id { get; set; }

        [Required]
        public string? Question { get; set; }
        [Required]
        public string? TrueAnswer { get; set; }
        [Required]
        public string? FalseAnswer { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
