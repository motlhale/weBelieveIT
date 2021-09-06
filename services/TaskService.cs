using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using weBelieveIT.Presentation;
using weBelieveIT.data;
using weBelieveIT.models;
using weBelieveIT.resultModels;

namespace weBelieveIT.services
{
    public class TaskService
    {
        private readonly ApplicationDbContext dbContext;

        public TaskService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateTask(TaskDto task)
        {
            dbContext.Task.Add(new Task
            {
                Description = task.Description,
                Name = task.Name,
                Priority = task.Priority,
                Status = task.Status,
                StartDate = task.StartDate,
                EndDate = task.EndDate
            });

            if(dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public List<TaskOtd> GetAllTasks()
        {
            var allTask = new List<TaskOtd>();
            dbContext.Task.Include(itm => itm.Member).ToList().ForEach(task =>
            {
                allTask.Add(new TaskOtd
                {
                    Description = task.Description,
                    EndDate = task.EndDate,
                    StartDate = task.StartDate,
                    ID = task.ID.ToString(),
                    Name = task.Name,
                    Priority = task.Priority,
                    Status = task.Status,
                    Member = GetTaskMemberOtds(task.Member)
                });
            });

            return allTask;
        }

        private TaskMemberOtd GetTaskMemberOtds(Member taskMember)
        {
            if(taskMember == null)
            {
                return null;
            }
            return new TaskMemberOtd
                {
                    Email = taskMember.Email,
                    FirstName = taskMember.FirstName,
                    LastName = taskMember.LastName,
                    Role = taskMember.Role
                };
        }

        public List<TaskOtd> GetTasksByStatus(Status status)
        {
            var tasks = new List<TaskOtd>();
            dbContext.Task.Include(itm => itm.Member).Where(itm => itm.Status == status).ToList().ForEach(task =>
            {
                tasks.Add(new TaskOtd
                {
                    Description = task.Description,
                    EndDate = task.EndDate,
                    StartDate = task.StartDate,
                    ID = task.ID.ToString(),
                    Name = task.Name,
                    Priority = task.Priority,
                    Status = task.Status,
                    Member = GetTaskMemberOtds(task.Member)
                });
            });
            return tasks;
        }

        public bool UpdateTask(Guid id,TaskDto task)
        {
            var taskInDb = dbContext.Task.FirstOrDefault(itm => itm.ID == id);
            if(taskInDb != null)
            {
                taskInDb.Description = task.Description?? taskInDb.Description;
                taskInDb.Name = task.Name ?? taskInDb.Name;
                taskInDb.StartDate = task.StartDate;
                taskInDb.EndDate = task.EndDate;
                taskInDb.Priority = task.Priority;
                taskInDb.Status = task.Status;

                dbContext.Task.Update(taskInDb);
                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateTaskStatus(Guid id,Status status)
        {
            var taskInDb = dbContext.Task.FirstOrDefault(itm => itm.ID == id);
            if(taskInDb != null)
            {
                taskInDb.Status = status;

                dbContext.Task.Update(taskInDb);
                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AssignTaskToMember(string jobNumber, Guid id,string email)
        {
            var job = dbContext.Job.Include(itm => itm.Tasks).FirstOrDefault(itm => itm.JobNumber == jobNumber);
            if(job != null)
            {
                var taskInDb = dbContext.Task.FirstOrDefault(itm => itm.ID == id);
                if(taskInDb != null)
                {
                    var member = dbContext.Member.Include(itm => itm.Tasks).FirstOrDefault(itm => itm.Email == email);
                    if(member != null)
                    {
                        member.Tasks.Add(taskInDb);
                        dbContext.Update(member);
                        if(dbContext.SaveChanges() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool RemoveMemberFromTask(string jobNumber, Guid id)
        {
            var job = dbContext.Job
                .Include(itm => itm.Tasks)
                .FirstOrDefault(itm => itm.JobNumber == jobNumber);
            if(job != null)
            {
                var taskInJob = job.Tasks.FirstOrDefault(itm => itm.ID == id);
                if(taskInJob != null)
                {
                    taskInJob.Member = null;
                    dbContext.Update(taskInJob);
                    if(dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*public bool CreateTaskToMember(string jobNumber, Task task,string email)
        {
            var job = dbContext.Job.Include(itm => itm.Members).FirstOrDefault(itm => itm.JobNumber == jobNumber);
            if(job != null)
            {
                var taskInDb = dbContext.Task.FirstOrDefault(itm => itm.ID == task.ID);
                if(taskInDb == null)
                {
                    var member = dbContext.Member.Include(itm => itm.Tasks).FirstOrDefault(itm => itm.Email == email);
                    if(member != null)
                    {
                        member.Tasks.Add(taskInDb);
                        dbContext.Update(member);
                        if(dbContext.SaveChanges() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }*/

        public bool RemoveTaskFromMember(string email, Guid Id)
        {
            var member = dbContext.Member
                .FirstOrDefault(itm => itm.Email == email);
            if(member != null)
            {
                var taskInDb = dbContext.Task.FirstOrDefault(itm => itm.ID == Id);
                if(taskInDb != null)
                {
                    member.Tasks.Remove(taskInDb);
                    dbContext.Update(member);
                    if(dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool RemoveTask(Guid id)
        {
            var taskInDb = dbContext.Task.FirstOrDefault(itm => itm.ID == id);
            if(taskInDb != null)
            {
                dbContext.Task.Remove(taskInDb);
                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}