using TodoList.Core.Domain.Base;

namespace TodoList.Core.Domain
{
    public class Todo : BaseEntity
    {
        public string Text { get; set; }
        public bool Done { get; set; }
        public DateTime Date { get; set; }
        public int Order { get; set; }
    }
}
