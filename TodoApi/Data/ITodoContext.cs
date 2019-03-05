using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Data
{
    public interface ITodoContext
    {
        IMongoCollection<TodoItem> Todos { get; }
    }
}
