using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll()
        {
            try
            {
                var tasks = await _taskService.GetAllTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                // Log the error here (e.g., ILogger)
                return StatusCode(500, "Erro interno ao processar a solicitação.");
            }
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetById(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null) return NotFound("Tarefa não encontrada.");
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno ao buscar a tarefa.");
            }
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskDto>> Create([FromBody] CreateTaskDto taskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var createdTask = await _taskService.CreateTaskAsync(taskDto);
                // Returns 201 Created with the location of the new resource
                return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
            }
            catch (ArgumentException ex) // Catch validation errors from Domain
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar a tarefa.");
            }
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto taskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _taskService.UpdateTaskAsync(id, taskDto);
                return NoContent(); // 204 No Content is standard for successful updates
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tarefa não encontrada.");
            }
            catch (InvalidOperationException ex) // Domain rule violation (e.g., Date error)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar a tarefa.");
            }
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tarefa não encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar a tarefa.");
            }
        }
    }
}