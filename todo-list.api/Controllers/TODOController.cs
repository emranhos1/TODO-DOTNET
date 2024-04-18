using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization.DataContracts;
using todo_list.api.Data;
using todo_list.api.Entities;

namespace todo_list.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TODOController : ControllerBase
    {
        private readonly ApplicationBDContext dBContext;

        public TODOController(ApplicationBDContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpPost("/save")]
        public async Task<IActionResult> AddTodoList(TODO todo)
        {
            var taskList = new TaskList();
            taskList.Task = todo.TaskList.Task;
            taskList.TaskDetails = todo.TaskList.TaskDetails;

            var newTodo = new TODO();
            newTodo.TaskList = taskList;
            newTodo.CreateDate = DateTime.Now;
            newTodo.CompletedDate = null;
            newTodo.IsDone = false;

            await dBContext.TODO.AddAsync(newTodo);
            var save = await dBContext.SaveChangesAsync();
            if (save == 2)
            {
                return Ok(newTodo);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("/get-all")]
        public async Task<IActionResult> AllTodoList()
        {
            var todoList  = await dBContext.TODO.Include(a => a.TaskList).ToListAsync();
            return Ok(todoList);
        }

        [HttpGet("/get-by-id/{id}")]
        public async Task<IActionResult> GetTodoList(int id)
        {
            var todoList = await dBContext.TODO.FindAsync(id);
            if (todoList != null)
            {
                return Ok(todoList);
            }
            else { 
                return NotFound("Task Not Found!");
            }
        }

        [HttpPut("/edit")]
        public async Task<IActionResult> Edit(TODO todoTask)
        {
            var hastodoTask = await dBContext.TODO.Include(a => a.TaskList).FirstOrDefaultAsync(a => a.Id == todoTask.Id);
            if (hastodoTask != null) {
                hastodoTask.TaskList.Task = todoTask.TaskList.Task;
                hastodoTask.TaskList.TaskDetails = todoTask.TaskList.TaskDetails;

                hastodoTask.CompletedDate = DateTime.Now;
                hastodoTask.IsDone = true;

                await dBContext.SaveChangesAsync();
                return Ok(hastodoTask);
            }
            else {
                return NotFound("Task Not Found!"); ;
            }
        }

        [HttpDelete("/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hastodoTask = await dBContext.TODO.Include(a => a.TaskList).FirstOrDefaultAsync(a => a.Id == id);
            if (hastodoTask != null)
            {
                dBContext.Remove(hastodoTask);
                await dBContext.SaveChangesAsync();

                return Ok("Task Deleted Successfully!");
            }
            else {
                return NotFound("Task Not Deleted!");
            }
        }
    }
}
