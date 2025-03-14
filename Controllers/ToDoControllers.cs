using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _repository;

        public TodoController(IConfiguration configuration)
        {
            _repository = new TodoRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _repository.GetTodos();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _repository.GetTodoById(id);
            if (todo == null) return NotFound("Data tidak ditemukan.");
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] Todo todo)
        {
            await _repository.CreateTodo(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest("ID di URL dan body tidak cocok.");
            }

            var existingTodo = await _repository.GetTodoById(id);
            if (existingTodo == null)
            {
                return NotFound("Data tidak ditemukan.");
            }

            await _repository.UpdateTodo(id, todo);
            return Ok("Data berhasil diperbarui.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var existingTodo = await _repository.GetTodoById(id);
            if (existingTodo == null)
            {
                return NotFound("Data tidak ditemukan.");
            }

            await _repository.DeleteTodo(id);
            return Ok("Data berhasil dihapus.");
        }
    }
}
