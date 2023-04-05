using Microsoft.AspNetCore.Mvc;
using TodoList.API.Controllers.Base;
using TodoList.Application.Services.Todos;
using TodoList.Application.Services.Todos.Dto;
using TodoList.Core.Exceptions;

namespace TodoList.API.Controllers
{
    public class TodoController : ApiControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            IEnumerable<TodoDto> result = await _todoService
                    .GetAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{todoId}")]
        public async Task<IActionResult> GetById(int todoId, CancellationToken cancellationToken = default)
        {
            TodoDto result = await _todoService
                    .GetAsync(todoId, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RequestCreateTask input, CancellationToken cancellationToken = default)
        {
            TodoDto result = await _todoService
                    .CreateAsync(input, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { todoId = result.Id }, result);
        }

        [HttpPost("complete")]
        public async Task<IActionResult> CompleteTask([FromBody]IEnumerable<RequestTodoCompletion> input,
                CancellationToken cancellationToken = default)
        {
            try
            {
                IEnumerable<ResponseTodoCompletion> result = await _todoService
                  .TaskCompletionAsync(input, cancellationToken);

                return Ok(result);
            }           
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{todoId}")]
        public async Task<IActionResult> Put(int todoId, [FromBody]RequestUpdateTask input, CancellationToken cancellationToken = default)
        {
            try
            {
                TodoDto result = await _todoService
                  .UpdateAsync(todoId, input, cancellationToken);

                return Ok(result);
            }
            catch (NotFoundException err)
            {
                return NotFound(err.Message);
            }
            catch (Exception)
            {
                throw;
            }          
        }

        

        [HttpDelete("{todoId}")]
        public async Task<IActionResult> Delete(int todoId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _todoService
                  .DeleteAsync(todoId, cancellationToken);

                return Ok();
            }
            catch (NotFoundException err)
            {
                return NotFound(err.Message);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
