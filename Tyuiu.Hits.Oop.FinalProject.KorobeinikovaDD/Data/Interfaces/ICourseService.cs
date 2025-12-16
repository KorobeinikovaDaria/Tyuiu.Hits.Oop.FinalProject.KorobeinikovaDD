using System.Collections.Generic;
using System.Threading.Tasks;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Models;


    public interface ICourseService
    {
    Task<IEnumerable<Course>> GetTaskItemsAsync();
    Task SaveAsync(Course item);

    Task<Course> GetTaskAsync(int id);

    Task DeleteAsync(int id);
}





