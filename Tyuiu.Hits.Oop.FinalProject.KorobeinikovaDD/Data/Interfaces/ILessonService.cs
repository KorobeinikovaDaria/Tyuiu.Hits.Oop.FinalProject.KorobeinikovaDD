using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetTaskItemsAsync();
        Task SaveAsync(Lesson item);

        Task<Lesson> GetTaskAsync(int id);

        Task DeleteAsync(int id);

    }
}
