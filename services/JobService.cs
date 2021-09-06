using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using weBelieveIT.data;
using weBelieveIT.models;
using weBelieveIT.Presentation;
using weBelieveIT.resultModels;

namespace weBelieveIT.services
{
    public class JobService
    {
        private readonly ApplicationDbContext dbContext;

        public JobService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string CreateJob(JobDto job)
        {
            var id = CreateJobNumber();
            var jobToAdd = new Job
            {
                JobNumber = id,
                Description = job.Description,
                Status = Status.Backlog,
                Tasks = GetTasksFromIDList(job.TaskIDS)
            };

            dbContext.Add(jobToAdd);
            if(dbContext.SaveChanges() > 0)
            {
                return id;
            }
            return null;
        }

        private List<Member> GetMembersFromEmailList(List<string> memberEmails)
        {
            var memberdToAdd = new List<Member>();
            foreach(var memberEmail in memberEmails)
            {
                memberdToAdd.Add(dbContext.Member.FirstOrDefault(itm => itm.Email == memberEmail));
            }
            return memberdToAdd;
        }

        private List<Task> GetTasksFromIDList(List<string> taskIds)
        {
            var tasksToAdd = new List<Task>();
            foreach(var taskId in taskIds)
            {
                var item = dbContext.Task.FirstOrDefault(itm => itm.ID == Guid.Parse(taskId));
                if(item != null)
                {
                    tasksToAdd.Add(item);
                }
            }
            return tasksToAdd;
        }

        public List<JobOtd> GetJobsByStatus(Status status)
        {
            var jobs = new List<JobOtd>();
            dbContext.Job.Include(itm => itm.Tasks).ThenInclude(itm => itm.Member).Where(itm => itm.Status == status).ToList().ForEach(job => 
            {
                jobs.Add(new JobOtd
                {
                    Description = job.Description,
                    JobNumber = job.JobNumber,
                    Status = job.Status,
                    Tasks = GetJobTaskOtds(job.Tasks)
                });
            });
            return jobs;
        }

        private List<JobTaskOtd> GetJobTaskOtds(List<Task> tasks)
        {
            var jobTasks = new List<JobTaskOtd>();
            tasks.ForEach(task => 
            {
                jobTasks.Add(new JobTaskOtd
                {
                    Description = task.Description,
                    EndDate = task.EndDate,
                    StartDate = task.StartDate,
                    Status = task.Status,
                    Name = task.Name,
                    Priority = task.Priority,
                    ID = task.ID.ToString()
                });
            });
            return jobTasks;
        }

        public bool AddTaskToJob(string jobNumber,Guid id)
        {
            var job = GetJob(jobNumber);
            if(job != null)
            {
                var taskInJob = job.Tasks.FirstOrDefault(itm => itm.ID == id);
                var taskInDb = dbContext.Task.FirstOrDefault(itm => itm.ID == id);
                if(taskInJob == null && taskInDb != null)
                {
                    job.Tasks.Add(taskInDb);
                    if(dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Job GetJob(string jobNumber)
        {
            return dbContext.Job
                .Include(itm => itm.Tasks)
                .FirstOrDefault(itm => itm.JobNumber == jobNumber);
        }

        public bool RemoveTaskFromJob(string jobNumber, Guid id)
        {
            var jobInDb = dbContext.Job.Include(itm => itm.Tasks).FirstOrDefault(itm => itm.JobNumber == jobNumber);
            if(jobInDb != null)
            {
                var taskInJob = jobInDb.Tasks.FirstOrDefault(itm => itm.ID == id);
                if(taskInJob != null)
                {
                    jobInDb.Tasks.Remove(taskInJob);
                    if(dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool UpdateJobStatus(string jobNumber, Status status)
        {
            var jobInDb = dbContext.Job.FirstOrDefault(itm => itm.JobNumber == jobNumber);
            if(jobInDb != null)
            {
                jobInDb.Status = status;
                dbContext.Job.Update(jobInDb);
                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RemoveJob(string jobNumber)
        {
            var jobInDb = dbContext.Job.FirstOrDefault(itm => itm.JobNumber == jobNumber);
            if(jobInDb != null)
            {
                dbContext.Remove(jobInDb);
                if(dbContext.SaveChanges() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private string CreateJobNumber()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 30)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}