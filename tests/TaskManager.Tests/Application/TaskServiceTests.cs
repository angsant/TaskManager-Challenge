using Moq;
using System.Threading.Tasks;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
//using TaskManager.Domain.Interfaces;
using Xunit;

namespace TaskManager.Tests.Application
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            // Setup the Mock Repository
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _taskService = new TaskService(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateTaskAsync_Should_Call_Repository_AddAsync()
        {
            // Arrange
            var createDto = new CreateTaskDto
            {
                Titulo = "Nova Tarefa de Teste",
                Descricao = "Testando o serviço"
            };

            // Act
            var result = await _taskService.CreateTaskAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createDto.Titulo, result.Titulo);

            // Verify that the repository's AddAsync method was called exactly once
            _taskRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<TaskItem>()), Times.Once);
        }
    }
}