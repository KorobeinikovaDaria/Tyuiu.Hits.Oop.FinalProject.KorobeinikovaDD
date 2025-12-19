using System.Threading.Tasks;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;

namespace Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Interfaces
{
    public interface ITestService
    {
        Task<List<Tests>> GetLessonsByCourseIdAsync(int courseId);
        Task SaveAsync(Tests item);

        Task<Tests> GetTaskAsync(int id);

        Task DeleteAsync(int id);
    }
}
