using Microsoft.EntityFrameworkCore;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;
using static ICourseService;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Services
{
    public class CourseService(ApplicationDbContext context) : ICourseService
    {
        public async Task<IEnumerable<Course>> GetTaskItemsAsync()
        {
            return await context.Courses.ToArrayAsync();
        }

        public async Task SaveAsync(Course item)
        {
            if (item.Id == 0)
            {
                item.StartDate = DateTime.Now;
                await context.Courses.AddAsync(item);
            }
            else
            {
                context.Courses.Update(item);
            }
            await context.SaveChangesAsync();
        }
        public async Task<Course> GetTaskAsync(int id)
        {
            return await context.Courses.FirstAsync(x => x.Id == id);
        }
        public async Task DeleteAsync(int id)
        {
            var taskItem = await context.Courses.FirstAsync(x => x.Id == id);
            context.Courses.Remove(taskItem);
            await context.SaveChangesAsync();
        }
    }
}
