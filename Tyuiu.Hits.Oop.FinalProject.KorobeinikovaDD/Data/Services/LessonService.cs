using Microsoft.EntityFrameworkCore;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Interfaces;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Services
{
    public class LessonService(ApplicationDbContext context) : ILessonService
    {
  
            public async Task<IEnumerable<Lesson>> GetTaskItemsAsync()
            {
                return await context.Lessons.ToArrayAsync();
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

