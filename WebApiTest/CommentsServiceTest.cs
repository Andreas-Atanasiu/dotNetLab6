using Lab2.Models;
using Lab2.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiTest
{
    class CommentsServiceTest
    {

        [Test]
        public void GetAllShouldReturnCorrectNumberOfPages()
        {
            var options = new DbContextOptionsBuilder<ExpensesDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllShouldReturnCorrectNumberOfPages")
                .Options;

            using (var context = new ExpensesDbContext(options))

            {
                var commentsService = new CommentsService(context);
                var expenseService = new ExpenseService(context);

                expenseService.Create(new Lab2.DTOs.PostExpenseDto
                {
                    Description = "test_description",
                    Sum = 999,
                    Location = "test_location",
                    Date = DateTime.Now,
                    Type = "Food",
                    Currency = "EUR",
                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Important = true,
                            Text = "test_comment",
                            Owner = null
                        }
                    },
                    

                    
                }, null);

                var allComments = commentsService.GetAll(1, string.Empty);
                Assert.AreEqual(1, allComments.NumberOfPages);
               
            }
        }

    }
}
