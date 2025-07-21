using Microsoft.EntityFrameworkCore;
using todoCli.Models;

namespace todoCli.Data {
    public class ApplicationDbContext : DbContext
    {        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {            options.UseSqlite("Data Source=todo.db");
        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
