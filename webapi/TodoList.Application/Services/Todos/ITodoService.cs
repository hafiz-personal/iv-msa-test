using TodoList.Application.Services.Todos.Dto;

namespace TodoList.Application.Services.Todos
{
    public interface ITodoService
    {
        Task<TodoDto> CreateAsync(RequestCreateTask input, CancellationToken cancellationToken);
        Task DeleteAsync(int todoId, CancellationToken cancellationToken);
        Task<IEnumerable<TodoDto>> GetAsync(CancellationToken cancellationToken);
        Task<TodoDto> GetAsync(int todoId, CancellationToken cancellationToken);
        Task<IEnumerable<ResponseTodoCompletion>> TaskCompletionAsync(IEnumerable<RequestTodoCompletion> input, CancellationToken cancellationToken);
        Task<TodoDto> UpdateAsync(int todoId, RequestUpdateTask input, CancellationToken cancellationToken);
    }
}