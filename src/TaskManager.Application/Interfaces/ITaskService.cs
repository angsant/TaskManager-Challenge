using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(int id);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto);
        Task UpdateTaskAsync(int id, UpdateTaskDto taskDto);
        Task DeleteTaskAsync(int id);
    }
}