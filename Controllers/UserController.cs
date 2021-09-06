using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using weBelieveIT.services;
using weBelieveIT.models;
using weBelieveIT.Presentation;

namespace weBelieveIT.Controllers
{
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        private readonly UserServices userServices;

        public UserController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet("GetMember")]
        public IActionResult GetMember(string email)
        {
            try
            {
                var result = userServices.GetMember(email);
                switch(result)
                {
                    case null:
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

        [HttpPost("CreateMember")]
        public IActionResult CreateMember([FromBody] MemberDto member)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = userServices.CreateMember(member);
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
            return BadRequest("Invalid body");
        }

        [HttpPut("UpdateMember")]
        public IActionResult UpdateMember([FromBody] MemberDto member)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var result = userServices.UpdateMember(member);
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
            return BadRequest("Invalid body");
        }

        [HttpDelete("RemoveMember")]
        public IActionResult RemoveMember(string email)
        {
            try
            {
                var result = userServices.RemoveMember(email);
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