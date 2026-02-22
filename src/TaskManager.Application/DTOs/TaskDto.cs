using System;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Status { get; set; } // Returned as string for readability, or Enum if preferred
    }
}