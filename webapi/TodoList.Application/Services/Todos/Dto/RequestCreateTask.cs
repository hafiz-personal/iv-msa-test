using System.ComponentModel.DataAnnotations;

namespace TodoList.Application.Services.Todos.Dto
{
    public class RequestCreateTask
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
