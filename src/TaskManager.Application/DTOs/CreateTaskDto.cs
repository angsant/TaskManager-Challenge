using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.DTOs
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } // [cite: 16, 21]

        public string? Descricao { get; set; } // [cite: 17]
    }
}