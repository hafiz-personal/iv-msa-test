namespace TodoList.Application.Services.Todos.Dto
{
    public class RequestTodoCompletion
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; }
    }
}
