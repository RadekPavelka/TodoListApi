using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoService
    {
        private readonly IMongoCollection<TodoItem> _todos;

        public TodoService(IConfiguration config)
        {
            var client = new MongoClient(config["Todos:ConnectionString"]);
            var database = client.GetDatabase("todos-db");
            _todos = database.GetCollection<TodoItem>("todos");
        }

        public List<TodoItem> Get()
        {
            return _todos.Find(todo => true).ToList();
        }

        public TodoItem Get(string id)
        {
            return _todos.Find<TodoItem>(todo => todo.Id == id).FirstOrDefault();
        }

        public TodoItem Create(TodoItem todo)
        {
            _todos.InsertOne(todo);
            return todo;
        }

        public void Update(string id, TodoItem todoIn)
        {
            _todos.ReplaceOne(todo => todo.Id == id, todoIn);
        }

        public void Remove(TodoItem todoOut)
        {
            _todos.DeleteOne(todo => todo.Id == todoOut.Id);
        }

        public void Remove(string id)
        {
            _todos.DeleteOne(todo => todo.Id == id);
        }
    }
}
