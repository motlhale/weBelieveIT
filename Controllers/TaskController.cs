using System;
using Microsoft.AspNetCore.Mvc;
using weBelieveIT.services;
using weBelieveIT.Presentation;

namespace weBelieveIT.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskService taskService;

        public TaskController(TaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks()
        {
            try
            {
                var result = this.taskService.GetAllTasks();
                switch(result.Count)
                {
                    case 0:
                        return NotFound(result);
                    default:
                        return Ok(result);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTaskByStatus")]
        public IActionResult GetTaskByStatus(Status status)
        {
            try
            {
                var result = this.taskService.GetTasksByStatus(status);
                switch(result.Count)
                {
                    case 0:
                        return NotFound(result);
                    default:
                        return Ok(result);
                }
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] TaskDto task)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = this.taskService.CreateTask(task);
                    switch(result)
                    {
                        case true:
                            return Ok(result);
                        default:
                            return Conflict(result);
                    }

                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Invalid body");
        }

        [HttpPost("AssignTaskToMember")]
        public IActionResult AssignTaskToMember(string jobNumber,string id, string email)
        {
            try
            {
                var result = this.taskService.AssignTaskToMember(jobNumber,Guid.Parse(id),email);
                switch(result)
                {
                    case true:
                        return Ok(result);
                    default:
                        return Conflict(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveTaskFromMember")]
        public IActionResult RemoveTaskFromMember(string email, string id)
        {
            try
            {
                var result = this.taskService.RemoveTaskFromMember(email,Guid.Parse(id));
                switch(result)
                {
                    case true:
                        return Ok(result);
                    default:
                        return Conflict(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask([FromBody] TaskDto task,string id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = this.taskService.UpdateTask(Guid.Parse(id),task);
                    switch(result)
                    {
                        case true:
                            return Ok(result);
                        case false:
                            return Conflict(result);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Invalid body");
        }

        [HttpPut("UpdateTaskStatus")]
        public IActionResult UpdateTaskStatus(string id,Status status)
        {
            try
            {
                var result = this.taskService.UpdateTaskStatus(Guid.Parse(id),status);
                switch(result)
                {
                    case true:
                        return Ok(result);
                    default:
                        return Conflict(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(string id)
        {
            try
            {
                var result = this.taskService.RemoveTask(Guid.Parse(id));
                switch (result)
                {
                    case true:
                        return Ok(result);
                    default:
                        return Conflict(result);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}