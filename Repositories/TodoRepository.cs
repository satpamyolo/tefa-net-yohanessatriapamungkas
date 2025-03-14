using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository
    {
        private readonly string _connectionString;

        public TodoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Todo>> GetTodos()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Todo>("SELECT * FROM todo");
        }

        public async Task<Todo> GetTodoById(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Todo>("SELECT * FROM todo WHERE id = @Id", new { Id = id });
        }

        public async Task<int> CreateTodo(Todo todo)
        {
            using var connection = CreateConnection();
            var query = "INSERT INTO todo (judul, description) VALUES (@Judul, @Description)";
            return await connection.ExecuteAsync(query, todo);
        }

        public async Task<int> UpdateTodo(int id, Todo todo)
        {
            using var connection = CreateConnection();
            var query = "UPDATE todo SET judul = @Judul, description = @Description WHERE id = @Id";
            return await connection.ExecuteAsync(query, new { todo.Judul, todo.Description, Id = id });
        }

        public async Task<int> DeleteTodo(int id)
        {
            using var connection = CreateConnection();
            var query = "DELETE FROM todo WHERE id = @Id";
            return await connection.ExecuteAsync(query, new { Id = id });
        }

        public IEnumerable<Todo> GetAll()
        {
            using var connection = new MySqlConnection(_connectionString);
            var result = connection.Query<Todo>("SELECT * FROM todo");
            return result ?? new List<Todo>(); // Hindari return null
        }

    }
}
