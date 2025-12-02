using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Важно вызвать базовый метод

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId);

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Курс 1", Description = "Описание курса 1", Level = 5 },
                new Course { Id = 2, Name = "Курс 2", Description = "Описание курса 2", Level = 8 },
                new Course { Id = 3, Name = "Курс 3", Description = "Описание курса 3", Level = 0 }
            );

            modelBuilder.Entity<Lesson>().HasData(
                new Lesson { Id = 1, Title = "Урок 1", Content = "Содержание урока", CourseId = 1 },
                new Lesson { Id = 2, Title = "Урок 2", Content = "Содержание урока", CourseId = 1 },
                new Lesson { Id = 3, Title = "Урок 3", Content = "Содержание урока", CourseId = 2 },
                new Lesson { Id = 4, Title = "Урок 4", Content = "Содержание урока", CourseId = 2 },
                new Lesson { Id = 5, Title = "Урок 5", Content = "Содержание урока", CourseId = 2 },
                new Lesson { Id = 6, Title = "Урок 6", Content = "Содержание урока", CourseId = 3 }
            );
        }
    }

}
