using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces // Namespace kept as Domain for strict adherence
{
    public interface ITaskRepository
    {
        Task<TaskItem> GetByIdAsync(int id); // [cite: 10]
        Task<IEnumerable<TaskItem>> GetAllAsync(); // [cite: 10]
        Task AddAsync(TaskItem task); // [cite: 9]
        Task UpdateAsync(TaskItem task); // [cite: 11]
        Task DeleteAsync(int id); // [cite: 12]
    }
}