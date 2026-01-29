using LexisApi.Utilities;
using System.ComponentModel.DataAnnotations;

namespace LexisApi.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }
        
        [Required]
        public StatusTask Status { get; set; } = StatusTask.New;

        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.Low;

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
