using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs
{
    public class UpdateTaskDto
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public TaskStatusEnum Status { get; set; } // [cite: 20]
    }
}