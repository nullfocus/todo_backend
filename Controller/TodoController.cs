using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql;
using System.Data.Common;

using TodoBackend;

namespace TodoAppBackend.Controller
{
    public class TodoController : Controller{


        [Route("tasks/today/{cur_date}")]
        public IActionResult GetTasksForToday(DateTime? cur_date)
        {
            return Query<Todo>("select * from get_today_tasks(@cur_date)", new { cur_date = cur_date });
        }

        [Route("tasks/backlog/{cur_date}")]
        public IActionResult GetAllBacklogTasks(DateTime? cur_date)
        {
            return Query<Todo>("select * from get_backlog_tasks(@cur_date)", new { cur_date = cur_date });

        }

        [Route("tasks/done")]
        public IActionResult GetDoneTasks()
        {
            return Query<Todo>("select * from get_done_tasks()");
            
        }

        [HttpPost]
        [Route("task/create")]
        public IActionResult CreateTask([FromBody] Todo todo)
        {
            return Execute("select * from insert_task(@name, @priority)", todo);
        }
        
        [HttpPost]
        [Route("task/{id}/update")]
        public IActionResult UpdateTask(int id, [FromBody] Todo todo)
        {
            if(todo == null)
                throw new Exception("null todo");

            return Execute("update tasks set name = @name, completed = @completed, priority = @priority where id = @id", todo);
        }

        [Route("task/{id}/delete")]
        public IActionResult DeleteTask(int id)
        {
            return Execute("delete from tasks where id = @id", new { id = id});
        }

        [Route("task/{id}/mark")]
        public IActionResult MarkTaskForToday(int id)
        {
            return Execute("select * from insert_dated_task(@id)", new { id = id});
        }

        [Route("task/{id}/unmark")]
        public IActionResult UnmarkTaskForToday(int id)
        {
            return Execute("select * from remove_dated_task(@id)", new { id = id});
        }

        [Route("task/{id}/up")]
        public IActionResult PrioritizeUp(int id)
        {
            return Execute("select shift_task(@id, true)", new { id = id});
        }

        [Route("task/{id}/down")]
        public IActionResult PrioritizeDown(int id)
        {
            return Execute("select shift_task(@id, false)", new { id = id});
        }
    }
}