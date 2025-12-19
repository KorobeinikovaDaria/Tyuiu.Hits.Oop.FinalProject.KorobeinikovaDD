using Microsoft.EntityFrameworkCore;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Interfaces;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Services
{
    public class TestService(ApplicationDbContext context) : ITestService
    {

        public async Task<List<Tests>> GetLessonsByCourseIdAsync(int courseId)
        {
            return await context.Tests
                .Where(Tests => Tests.CourseId == courseId)
                .ToListAsync(); 
        }

        public async Task SaveAsync(Tests item)
        {
            if (item.Id == 0)
            {
                await context.Tests.AddAsync(item);
            }
            else
            {
                context.Tests.Update(item);
            }
            await context.SaveChangesAsync();
        }
        public async Task<Tests> GetTaskAsync(int id)
        {
            return await context.Tests.FirstAsync(x => x.Id == id);
        }
        public async Task DeleteAsync(int id)
        {
            var taskItem = await context.Tests.FirstAsync(x => x.Id == id);
            context.Tests.Remove(taskItem);
            await context.SaveChangesAsync();
        }
    }
}
