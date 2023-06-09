﻿using Microsoft.EntityFrameworkCore;
using TodoList.Core.Domain;

namespace TodoList.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
