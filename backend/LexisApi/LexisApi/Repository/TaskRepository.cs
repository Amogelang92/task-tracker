using LexisApi.DTO;
using LexisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LexisApi.Repository
{

    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAysnc(string? search, string? sort);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskItem taskItem);
        Task UpdateTaskAsync(TaskItem taskItem);
        Task DeleteTaskAsync(TaskItem taskItem);
    }

    public class TaskRepository : ITaskRepository
    {
        private DataContext _context { get; set; }

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddTaskAsync(TaskItem taskItem)
        {
            _context.Tasks.Add(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAysnc(string? search, string? sort)
        {
            var result = _context.Tasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                result = result.Where(x => x.Title.Contains(search) || (x.Description != String.Empty && x.Description.Contains(search)));
            }

            if (!string.IsNullOrEmpty(sort) && sort.Equals("dueDate:desc", StringComparison.OrdinalIgnoreCase))
            {
                result = result.OrderByDescending(t => t.DueDate);
            }
            else
            {
                result = result.OrderBy(t => t.DueDate);
            }

            return await result.ToListAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task UpdateTaskAsync(TaskItem taskItem)
        {
            _context.Tasks.Update(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskItem taskItem)
        {
            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();
        }

    }
}
