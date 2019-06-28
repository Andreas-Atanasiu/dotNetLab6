using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.DTOs
{
    public class ExpenseDetails
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Sum { get; set; }

        public DateTime Date { get; set; }

        public string Currency { get; set; }

        public string Location { get; set; }

        public TypeEnum Type { get; set; }

        public int NumberOfComments { get; set; }

        public IEnumerable<GetCommentsDto> Comments { get; set; }

        public static ExpenseDetails DtoFromModel(Expense expense)
        {
            return new ExpenseDetails
            {
                Id = expense.Id,
                Description = expense.Description,
                Sum = expense.Sum,
                Date = expense.Date,
                Location = expense.Location,
                Currency = expense.Currency,
                Type = expense.Type,

                Comments = expense.Comments.Select(comment => new GetCommentsDto
                {
                    Id = comment.Id,
                    ExpenseId = expense.Id,
                    Text = comment.Text,
                    Important = comment.Important


                }),
                NumberOfComments = expense.Comments.Count

            };
        }

    }
}
