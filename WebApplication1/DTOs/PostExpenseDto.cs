using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.DTOs
{
    public class PostExpenseDto
    {
        public string Description { get; set; }

        public int Sum { get; set; }

        public string Location { get; set; }

        public DateTime Date { get; set; }

        public string Currency { get; set; }

        public string Type { get; set; }

        public List<Comment> Comments { get; set; }

        public static Expense ModelFromDto(PostExpenseDto expenseDto)
        {
            TypeEnum type = TypeEnum.Other;

            if (expenseDto.Type == "Food")
            {
                type = TypeEnum.Food;
            }
            else if (expenseDto.Type == "Utilities")
            {
                type = TypeEnum.Utilities;
            }
            else if (expenseDto.Type == "Transportation")
            {
                type = TypeEnum.Transportation;
            }
            else if (expenseDto.Type == "Outing")
            {
                type = TypeEnum.Outing;
            }
            else if (expenseDto.Type == "Groceries")
            {
                type = TypeEnum.Groceries;
            }
            else if (expenseDto.Type == "Clothes")
            {
                type = TypeEnum.Clothes;
            }
            else if (expenseDto.Type == "Electronics")
            {
                type = TypeEnum.Electronics;
            }
            return new Expense
            {
                Description = expenseDto.Description,
                Sum = expenseDto.Sum,
                Location = expenseDto.Location,
                Date = expenseDto.Date,
                Currency = expenseDto.Currency,
                Type = type,
                Comments = expenseDto.Comments
                
            };
        }
    }
}
