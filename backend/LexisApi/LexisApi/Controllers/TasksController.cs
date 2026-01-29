using LexisApi.DTO;
using LexisApi.Models;
using LexisApi.Repository;
using LexisApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace LexisApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] string? sort)
        {
            return Ok( await _taskService.GetTasksAsync(search,sort));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskItemDTO taskItemDTO)
        {
            await _taskService.CreateTaskAsync(taskItemDTO);

            return CreatedAtAction(nameof(Get),new {id = taskItemDTO.Title}, taskItemDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItemDTO taskItemDTO)
        {
            var update = await _taskService.UpdateTaskAsync(id, taskItemDTO);
            return Ok(update);        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
