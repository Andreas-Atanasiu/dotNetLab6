using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.DTOs
{
    public class GetCommentsDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool Important { get; set; }

        public int? ExpenseId { get; set; }

        public static GetCommentsDto FromComment(Comment comment)
        {
            return new GetCommentsDto
            {
                Id = comment.Id,
                ExpenseId = comment.Expense?.Id,
                Important = comment.Important,
                Text = comment.Text,

            };
        }
    }
}
