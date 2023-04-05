namespace TodoList.Application.Services.Todos.Dto
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
        public int Order { get; set; }
    }
}
