using Lab2.DTOs;
using Lab2.Models;
using Lab2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {

        private IExpenseService expenseService;
        private IUsersService usersService;

        public ExpenseController(IExpenseService expenseService, IUsersService usersService)
        {
            this.expenseService = expenseService;
            this.usersService = usersService;
        }


        // [HttpGet]
        // public IEnumerable<Expense> GetExpenses()
        // {
        //     return context.Expenses;
        // }

        [Authorize(Roles = "Regular, Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {   
            var found = expenseService.GetByIdNew(id);

            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [Authorize(Roles = "Regular, Admin")]
        [HttpPost]
        public void Post([FromBody] PostExpenseDto expense)
        {
            User addedBy = usersService.GetCurrentUser(HttpContext);
            //if(addedBy.UserRole ==  UserRole.UserManager)
            //{
            //    return Forbid();
            //}
            expenseService.Create(expense, addedBy  );   
        }

        [Authorize(Roles = "Regular, Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {
            var result = expenseService.Upsert(id, expense);
            return Ok(result);
        }


        [Authorize(Roles = "Regular, Admin")]
    
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var result = expenseService.Delete(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        /// <summary>
        /// Get all the expenses
        /// </summary>
        /// <param name="from">Optional, filter by min</param>
        /// <param name="to">Optional, filter by max</param>
        /// <param name="type">Optional, filter by type</param>
        /// <returns></returns>
        [HttpGet]
        public PaginatedList<GetExpenseDto> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]TypeEnum? type, [FromQuery]int page = 1)
        {
            page = Math.Max(page, 1);
            return expenseService.GetAll(page, from, to, type);

        }


    }
}

