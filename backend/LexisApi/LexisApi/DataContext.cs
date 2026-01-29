using Microsoft.EntityFrameworkCore;
using LexisApi.Models;
using LexisApi.Utilities;

namespace LexisApi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}
