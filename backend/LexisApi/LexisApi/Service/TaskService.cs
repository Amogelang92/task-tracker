using LexisApi.DTO;
using LexisApi.Models;
using LexisApi.Repository;

namespace LexisApi.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDTO>> GetTasksAsync(string? search, string? sort);
        Task<TaskItemDTO?> GetTaskByIdAsync(int id);
        Task<TaskItemDTO> CreateTaskAsync(TaskItemDTO dto);
        Task<TaskItemDTO> UpdateTaskAsync(int id, TaskItemDTO dto);
        Task DeleteTaskAsync(int id);
    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<TaskItemDTO> CreateTaskAsync(TaskItemDTO dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddTaskAsync(task);

            return new TaskItemDTO
            {
            
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt
            };
        }

        public async Task DeleteTaskAsync(int id)
        {
            var taskItem = await _repo.GetTaskByIdAsync(id);

            if (taskItem == null)
                throw new KeyNotFoundException("Task not found");

            await _repo.DeleteTaskAsync(taskItem);
        }

        public async Task<TaskItemDTO?> GetTaskByIdAsync(int id)
        {
            var taskItem = await _repo.GetTaskByIdAsync(id);

            if (taskItem == null)
                return null;

            return new TaskItemDTO
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                Status = taskItem.Status,
                Priority = taskItem.Priority,
                DueDate = taskItem.DueDate,
                CreatedAt = taskItem.CreatedAt
            };
        }

        public async Task<IEnumerable<TaskItemDTO>> GetTasksAsync(string? search, string? sort)
        {
            var tasks = await _repo.GetAllTasksAysnc(search,sort);

            return tasks.Select(t => new TaskItemDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt
            });
        }

        public async Task<TaskItemDTO> UpdateTaskAsync(int id, TaskItemDTO dto)
        {
            var taskItem = await _repo.GetTaskByIdAsync(id);

            if (taskItem == null)
                throw new KeyNotFoundException("Task not found");

            dto.Id = id;
            taskItem.Id = id;
            taskItem.Title = dto.Title;
            taskItem.Description = dto.Description;
            taskItem.Status = dto.Status;
            taskItem.Priority = dto.Priority;
            taskItem.DueDate = dto.DueDate;
            taskItem.CreatedAt = dto.CreatedAt;

            await _repo.UpdateTaskAsync(taskItem);

            return dto;
        }
    }
}
