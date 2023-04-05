using System.ComponentModel.DataAnnotations;

namespace TodoList.Core.Domain.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
