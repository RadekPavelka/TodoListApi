using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoContext : ITodoContext
    {
        private readonly IMongoDatabase _db;

        public TodoContext(IOptions<Settings> options) 
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<TodoItem> Todos => _db.GetCollection<TodoItem>("todos");
    }
}