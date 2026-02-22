using System;
using TaskManager.Domain.Entities;
using Xunit;

namespace TaskManager.Tests.Domain
{
    public class TaskItemTests
    {
        [Fact]
        public void Constructor_Should_CreateTask_When_Valid()
        {
            // Arrange
            string validTitle = "Estudar xUnit";
            string validDescription = "Criar testes unitários para a API";

            // Act
            var task = new TaskItem(validTitle, validDescription);

            // Assert
            Assert.Equal(validTitle, task.Titulo);
            Assert.Equal(validDescription, task.Descricao);
            Assert.Equal(TaskManager.Domain.Enums.TaskStatusEnum.Pendente, task.Status);
            Assert.True(task.DataCriacao <= DateTime.UtcNow);
        }

        [Fact]
        public void Constructor_Should_ThrowArgumentException_When_TituloIsEmpty()
        {
            // Arrange
            string emptyTitle = "";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new TaskItem(emptyTitle, "Descrição"));
            Assert.Equal("O título é obrigatório.", exception.Message);
        }

        [Fact]
        public void Constructor_Should_ThrowArgumentException_When_TituloExceeds100Characters()
        {
            // Arrange
            string longTitle = new string('A', 101); // 101 characters

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new TaskItem(longTitle, "Descrição"));
            Assert.Equal("O título deve ter no máximo 100 caracteres.", exception.Message);
        }
    }
}