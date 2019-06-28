using Lab2.DTOs;
using Lab2.Models;
using Lab2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRoleService roleService;

        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        public IEnumerable<RolePostModel> GetAll()
        {
            return roleService.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var found = roleService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [HttpPost]
        public void Post([FromBody] RolePostModel role)
        {
            roleService.Create(role);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserRole role)
        {
            var roleToUpdate = roleService.Upsert(id, role);
            return Ok(roleToUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = roleService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
