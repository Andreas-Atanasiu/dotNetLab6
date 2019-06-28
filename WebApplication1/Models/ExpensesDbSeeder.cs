using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class ExpensesDbSeeder
    {
        public static void Initialize(ExpensesDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Expenses.Any())
            {
                return;
            }

            context.Expenses.AddRange(
                new Expense
                {
                    Description = "t-shirt",
                    Sum = 99,
                    Location = "Cluj",
                    Date = new DateTime(2019, 05, 20, 11, 40, 0),
                    Currency = "RON",
                    Type = TypeEnum.Clothes
                },
                new Expense
                {
                    Description = "food",
                    Sum = 50,
                    Location = "Cluj",
                    Date = new DateTime(2019, 05, 20, 12, 10, 0),
                    Currency = "RON",
                    Type = TypeEnum.Groceries
                }
            );
            context.SaveChanges();
        }
    }
}
