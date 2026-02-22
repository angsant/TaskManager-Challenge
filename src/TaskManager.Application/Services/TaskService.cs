using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        // Dependency Injection: The service needs a Repository to work.
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _repository.GetAllAsync();

            // Mapping Entity -> DTO
            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataCriacao = t.DataCriacao,
                DataConclusao = t.DataConclusao,
                Status = t.Status.ToString()
            });
        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Titulo = task.Titulo,
                Descricao = task.Descricao,
                DataCriacao = task.DataCriacao,
                DataConclusao = task.DataConclusao,
                Status = task.Status.ToString()
            };
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto)
        {
            // Business Logic: Create the entity (Validation happens inside the Entity constructor)
            var task = new TaskItem(taskDto.Titulo, taskDto.Descricao);

            await _repository.AddAsync(task);

            // Return the created object as DTO
            return new TaskDto
            {
                Id = task.Id,
                Titulo = task.Titulo,
                Descricao = task.Descricao,
                DataCriacao = task.DataCriacao,
                Status = task.Status.ToString()
            };
        }

        public async Task UpdateTaskAsync(int id, UpdateTaskDto taskDto)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) throw new KeyNotFoundException("Tarefa não encontrada.");

            // Update fields
            // Note: We use the Entity's method to ensure consistency
            task.Update(taskDto.Titulo, taskDto.Descricao, taskDto.Status);

            // Special logic for completion if status changed to 'Concluida'
            if (taskDto.Status == Domain.Enums.TaskStatusEnum.Concluida)
            {
                task.Concluir(); // Enforces the "Date > CreationDate" rule [cite: 22]
            }

            await _repository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) throw new KeyNotFoundException("Tarefa não encontrada.");

            await _repository.DeleteAsync(id);
        }
    }
}