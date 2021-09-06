using System;
using Microsoft.AspNetCore.Mvc;
using weBelieveIT.services;
using weBelieveIT.Presentation;

namespace weBelieveIT.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        private readonly TeamService teamService;
        public TeamController(TeamService teamService)
        {
            this.teamService = teamService;
        }

        [HttpGet("GetAllTeams")]
        public IActionResult GetAllTeams()
        {
            try
            {
                var result = this.teamService.GetAllTeams();
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

        [HttpPost("CreateTeam")]
        public IActionResult CreateTeam([FromBody] TeamDto team)
        {
            try
            {
                var result = this.teamService.CreateTeam(team);
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

        [HttpPut("AddMemberToTeam")]
        public IActionResult AddMemberToTeam(string id, string email)
        {
            try
            {
                var result = this.teamService.AddMemberToTeam(Guid.Parse(id),email);
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

        [HttpDelete("DeleteTeam")]
        public IActionResult DeleteTeam(string id)
        {
            try
            {
                var result = this.teamService.RemoveTeam(Guid.Parse(id));
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
    }
}