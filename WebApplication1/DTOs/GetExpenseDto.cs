using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.DTOs
{
    public class GetExpenseDto
    {
        public string Description { get; set; }

        public int Sum { get; set; }

        public DateTime Date { get; set; }

        public string Currency { get; set; }

        public TypeEnum Type { get; set; }

        public int NumberOfComments { get; set; }
       

        public static GetExpenseDto DtoFromModel(Expense expense)
        {
            return new GetExpenseDto
            {
                Description = expense.Description,
                Sum = expense.Sum,
                Date = expense.Date,
                Currency = expense.Currency,
                Type = expense.Type,
                NumberOfComments = expense.Comments.Count
            };
        }
    }
}
