using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Services
{
    public class MemoryDataService : ICourseService
    {
        private static IEnumerable<Course> tasks = new List<Course>
        {
            new Course { Id = 1, Name = "Курс 1", Description = "Описание курса 1", Level = 5 },
            new Course { Id = 2, Name = "Курс 2", Description = "Описание курса 2", Level = 8 },
            new Course { Id = 3, Name = "Курс 3", Description = "Описание курса 3", Level = 0 }
        };
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetTaskItemsAsync()
        {
            await Task.Delay(1000);
            return await Task.FromResult(tasks);
        }

        public Task SaveAsync(Course item)
        {
            throw new NotImplementedException();
        }
    }
}
