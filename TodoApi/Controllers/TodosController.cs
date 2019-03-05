using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{   
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }


        // GET: api/Todos
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _todoRepository.GetAllTodos());
        }

        // GET: api/Todos/id
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<TodoItem>> Get(string id)
        {
            var todo = await _todoRepository.GetTodo(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST: api/Todos
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Create(TodoItem todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _todoRepository.CreateTodo(todo);
            return CreatedAtRoute("Get", new { id = todo.Id }, todo);
        }


        // PUT: api/Todos/id
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, TodoItem todoIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var todoFromDb = await _todoRepository.GetTodo(id);
            if (todoFromDb == null)
            {
                return NotFound();
            }

            todoIn.Id = todoFromDb.Id;
            await _todoRepository.UpdateTodo(id, todoIn);
            return NoContent();
        }

        // DELETE: api/Todos/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var todo = await _todoRepository.GetTodo(id);
            if (todo == null)
            {
                return NotFound();
            }
            await _todoRepository.RemoveTodo(todo.Id);
            return NoContent();
        }
    }
}
