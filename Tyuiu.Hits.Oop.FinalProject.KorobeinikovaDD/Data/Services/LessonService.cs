using Microsoft.EntityFrameworkCore;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Interfaces;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Services
{
    public class LessonService(ApplicationDbContext context) : ILessonService
    {

        public async Task<List<Lesson>> GetLessonsByCourseIdAsync(int courseId)
        {
            return await context.Lessons
                .Where(lesson => lesson.CourseId == courseId)
                .ToListAsync(); // Здесь мы получаем List<Lesson>
        }


        public async Task SaveAsync(Lesson item)
            {
                if (item.Id == 0)
                {
                    await context.Lessons.AddAsync(item);
                }
                else
                {
                    context.Lessons.Update(item);
                }
                await context.SaveChangesAsync();
            }
            public async Task<Lesson> GetTaskAsync(int id)
            {
                return await context.Lessons.FirstAsync(x => x.Id == id);
            }
            public async Task DeleteAsync(int id)
            {
                var taskItem = await context.Lessons.FirstAsync(x => x.Id == id);
                context.Lessons.Remove(taskItem);
                await context.SaveChangesAsync();
            }
        }
    }

