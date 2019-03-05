using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllTodos();
        Task<TodoItem> GetTodo(string id);
        Task CreateTodo(TodoItem todo);
        Task UpdateTodo(string id, TodoItem todoIn);
        Task RemoveTodo(TodoItem todoOut);
        Task RemoveTodo(string id);
    }
}