using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodosController(TodoService todoService)
        {
            _todoService = todoService;
        }


        // GET: api/Todos
        [HttpGet]
        public ActionResult<List<TodoItem>> Get()
        {
            return _todoService.Get();
        }

        // GET: api/Todos/id
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<TodoItem> Get(string id)
        {
            var todo = _todoService.Get(id);
            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // POST: api/Todos
        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem todo)
        {
            _todoService.Create(todo);
            return CreatedAtRoute("Get", new { id = todo.Id.ToString() }, todo);
        }


        // PUT: api/Todos/id
        [HttpPut("{id}")]
        public IActionResult Update(string id, TodoItem todoIn)
        {
            var todo = _todoService.Get(id);
            if (todo == null)
            {
                return NotFound();
            }
            _todoService.Update(id, todoIn);
            return NoContent();
        }

        // DELETE: api/Todos/id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = _todoService.Get(id);
            if (todo == null)
            {
                return NotFound();
            }
            _todoService.Remove(todo.Id);
            return NoContent();
        }
    }
}
