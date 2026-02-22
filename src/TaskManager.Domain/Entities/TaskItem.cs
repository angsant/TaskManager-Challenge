using System;
using System.ComponentModel.DataAnnotations; // For basic validations
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        // [cite: 15] Id (int, auto incrementado - DB handles increment, Domain handles type)
        public int Id { get; private set; }

        // [cite: 16, 21] Título (string, obrigatório, max 100 caracteres)
        // Validations can also be enforced via FluentValidation or Data Annotations in the API DTOs,
        // but the Domain entity must safeguard its own integrity.
        public string Titulo { get; private set; }

        // [cite: 17] Descrição (string, opcional)
        public string? Descricao { get; private set; }

        //  Data de Criação (DateTime, gerado automaticamente)
        public DateTime DataCriacao { get; private set; }

        // [cite: 19] Data de Conclusão (DateTime?, opcional)
        public DateTime? DataConclusao { get; private set; }

        //  Status (enum)
        public TaskStatusEnum Status { get; private set; }

        // Constructor for EF Core (needs an empty constructor)
        protected TaskItem() { }

        // Public Constructor for creating a NEW task
        public TaskItem(string titulo, string? descricao)
        {
            SetTitulo(titulo);
            Descricao = descricao;

            //  Gerado automaticamente
            DataCriacao = DateTime.UtcNow;
            Status = TaskStatusEnum.Pendente;
        }

        // Method to update the task (Clean Code: SRP - methods responsible for changing state)
        public void Update(string titulo, string? descricao, TaskStatusEnum status)
        {
            SetTitulo(titulo);
            Descricao = descricao;
            Status = status;
        }

        // Method specifically to complete the task and validate the date
        public void Concluir()
        {
            var now = DateTime.UtcNow;

            //  A Data de Conclusão não pode ser anterior à Data de Criação.
            if (now < DataCriacao)
            {
                throw new InvalidOperationException("A data de conclusão não pode ser anterior à data de criação.");
            }

            DataConclusao = now;
            Status = TaskStatusEnum.Concluida;
        }

        // Helper to validate Title rule 
        private void SetTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título é obrigatório.");

            if (titulo.Length > 100)
                throw new ArgumentException("O título deve ter no máximo 100 caracteres.");

            Titulo = titulo;
        }
    }
}