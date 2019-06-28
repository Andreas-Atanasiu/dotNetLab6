using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.DTOs;
using Lab2.Models;
using Lab2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]PostLoginDto login) 
        {
            var user = _userService.Authenticate(login.Username, login.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            
            return Ok(user);

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]PostUserDto registerModel)
        {
            var errors = _userService.Register(registerModel); //, out User user);
            if (errors != null)
            {
                return BadRequest(errors);
            }
            return Ok(); //user);
        }

        [Authorize(Roles = "Admin,UserManager")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Roles = "Admin,UserManager")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]User user)
        {
            User userToBeUpdated = _userService.GetUserById(id);

            
            if (userToBeUpdated == null)
            {
                return NotFound();
            }

            //var currentUser = _userService.GetCurrentUser(HttpContext);
            //
            //if (currentUser.UserRole == UserRole.UserManager && userToBeUpdated.UserRole == UserRole.Admin)
            // {
            //    return Unauthorized();
            // }

            var result = _userService.UpdateUserNoRoleChange(id, user);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,UserManager")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var currentUser = _userService.GetCurrentUser(HttpContext);

            var userToDelete = _userService.GetUserById(id);

            //if (currentUser.UserRole == UserRole.UserManager)
            //{
                if (userToDelete.UserRole == UserRole.Admin)
                {
                    return Unauthorized();
                }

                //int monthsDiff = DateTimeUtils.GetMonthDifference(currentUser.DateAdded, DateTime.Now);
                //
                //if (userToDelete.UserRole == UserRole.UserManager && monthsDiff < 6)
                //{
                //    return Unauthorized();
                //}
                //}

            var result = _userService.DeleteUser(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
