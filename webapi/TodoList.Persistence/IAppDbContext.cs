using Microsoft.EntityFrameworkCore;
using TodoList.Core.Domain;

namespace TodoList.Persistence
{
    public interface IAppDbContext
    {
        DbSet<Todo> Todos { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
