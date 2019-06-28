using Lab2.DTOs;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public class CommentsService : ICommentService
    {

        private ExpensesDbContext context;

        public CommentsService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public PaginatedList<GetCommentsDto> GetAll(int page, string filterString)
        {
            IQueryable<Comment> result = context
                .Comments
                .Where(c => string.IsNullOrEmpty(filterString) || c.Text.Contains(filterString))
                .OrderBy(c => c.Id)
                .Include(c => c.Expense);
            var paginatedResult = new PaginatedList<GetCommentsDto>();
            paginatedResult.CurrentPage = page;

            paginatedResult.NumberOfPages = (result.Count() - 1) / PaginatedList<GetCommentsDto>.EntriesPerPage + 1;
            result = result
                .Skip((page - 1) * PaginatedList<GetCommentsDto>.EntriesPerPage)
                .Take(PaginatedList<GetCommentsDto>.EntriesPerPage);
            paginatedResult.Entries = result.Select(c => GetCommentsDto.FromComment(c)).ToList();

            return paginatedResult;
        }

        
     
    }
}
