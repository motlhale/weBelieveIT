using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using weBelieveIT.services;
using weBelieveIT.models;
using weBelieveIT.Presentation;

namespace weBelieveIT.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly JobService jobService;

        public JobController(JobService jobService)
        {
            this.jobService = jobService;
        }

        [HttpGet("GetJobsByStatus")]
        public IActionResult GetJobsByStatus(Status status)
        {
            try
            {
                var result = jobService.GetJobsByStatus(status);
                if(result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateJob")]
        public IActionResult CreateJob([FromBody] JobDto job)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = jobService.CreateJob(job);
                    switch(result)
                    {
                        case null:
                            return Conflict(result);
                        default:
                            return Ok(result);
                    }
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Invalid body");
        }

        [HttpDelete("RemoveTaskFromJob")]
        public IActionResult RemoveTaskFromJob(string jobNumber, string id)
        {
            try
            {
                var result = jobService.RemoveTaskFromJob(jobNumber,Guid.Parse(id));
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

        [HttpPut("UpdateJobStatus")]
        public IActionResult UpdateJobStatus(string jobNumber, Status status)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = jobService.UpdateJobStatus(jobNumber,status);
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

        [HttpDelete("RemoveJob")]
        public IActionResult RemoveJob(string jobNumber)
        {
            try
            {
                var result = jobService.RemoveJob(jobNumber);
                switch(result)
                {
                    case true:
                        return Ok(result);
                    default:
                        return Conflict(result);
                }
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}