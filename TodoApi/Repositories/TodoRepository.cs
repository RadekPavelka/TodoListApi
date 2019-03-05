using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ITodoContext _context;
        public TodoRepository(ITodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItem>> GetAllTodos()
        {
            return await _context
                .Todos
                .Find(todo => true)
                .ToListAsync();
        }

        public async Task<TodoItem> GetTodo(string id)
        {
            return await _context
                .Todos
                .Find(todo => todo.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CreateTodo(TodoItem todo)
        {
            await _context.Todos.InsertOneAsync(todo);
        }

        public async Task UpdateTodo(string id, TodoItem todoIn)
        {
            await _context.Todos.ReplaceOneAsync(todo => todo.Id == id, todoIn);
        }

        public async Task RemoveTodo(TodoItem todoOut)
        {
            await _context.Todos.DeleteOneAsync(todo => todo.Id == todoOut.Id);
        }

        public async Task RemoveTodo(string id)
        {
            await _context.Todos.DeleteOneAsync(todo => todo.Id == id);
        }
    }
}
