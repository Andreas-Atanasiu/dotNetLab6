using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.DTOs;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Services
{
    public class ExpenseService : IExpenseService
    {
        private ExpensesDbContext context;

        public ExpenseService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public PaginatedList<GetExpenseDto> GetAll(int page, DateTime? from = null, DateTime? to = null, TypeEnum? type = null)
        {
            IQueryable<Expense> result = context.Expenses
                                        .OrderBy(expense => expense.Id)
                                        .Include(expense => expense.Comments);


            PaginatedList<GetExpenseDto> paginatedResult = new PaginatedList<GetExpenseDto>();
            paginatedResult.CurrentPage = page;

            if (from != null)
            {
                result = result.Where(expense => expense.Date >= from);
            }

            if (to != null)
            {
                result = result.Where(expense => expense.Date <= to);
            }

            if (type != null)
            {
                result = result.Where(expense => expense.Type == type);
            }

            paginatedResult.NumberOfPages = (result.Count() - 1) / PaginatedList<GetExpenseDto>.EntriesPerPage;
            result = result
                     .Skip((page - 1) * PaginatedList<GetExpenseDto>.EntriesPerPage)
                     .Take(PaginatedList<GetExpenseDto>.EntriesPerPage);

            paginatedResult.Entries = result.Select(expense => GetExpenseDto.DtoFromModel(expense)).ToList();


            return paginatedResult;
        }

        //public Expense GetById(int id)
        //{
        //    return context.Expenses.Include(ex => ex.Comments).FirstOrDefault(ex => ex.Id == id);
        //}

        public ExpenseDetails GetByIdNew(int id)
        {
            var result = context.Expenses.Include(ex => ex.Comments).FirstOrDefault(ex => ex.Id == id);
            ExpenseDetails expenseDetails = ExpenseDetails.DtoFromModel(result);

            return expenseDetails;
        }

        public Expense Create(PostExpenseDto expenseDto, User addedBy)
        {
            Expense expenseModel = PostExpenseDto.ModelFromDto(expenseDto);

            expenseModel.Owner = addedBy;

            context.Expenses.Add(expenseModel);
            context.SaveChanges();
            return expenseModel;
        }

        public Expense Upsert(int id, Expense expense)
        {
            var existing = context.Expenses.AsNoTracking().FirstOrDefault(ex => ex.Id == id);

            if (existing == null)
            {
                context.Expenses.Add(expense);
                context.SaveChanges();
                return expense;
            }

            expense.Id = id;
            context.Expenses.Update(expense);
            context.SaveChanges();
            return expense;
        }

        public Expense Delete(int id)
        {
            var existing = context.Expenses
                .Include(ex => ex.Comments)
                .FirstOrDefault(expense => expense.Id == id);

            if (existing == null)
            {
                return null;
            }

            context.Expenses.Remove(existing);
            context.SaveChanges();
            return existing;
        }

    }
}
