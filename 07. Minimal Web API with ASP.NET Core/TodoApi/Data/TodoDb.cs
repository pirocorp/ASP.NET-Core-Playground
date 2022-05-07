namespace TodoApi.Data
{
    using Microsoft.EntityFrameworkCore;

    using TodoApi.Data.Models;

    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<Todo> Todos => this.Set<Todo>();
    }
}
