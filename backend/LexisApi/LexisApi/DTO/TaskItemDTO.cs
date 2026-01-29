using LexisApi.Utilities;
using System.ComponentModel.DataAnnotations;

namespace LexisApi.DTO
{
    public class TaskItemDTO
    {
   
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public StatusTask Status { get; set; } 

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
