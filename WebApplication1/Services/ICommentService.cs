using Lab2.DTOs;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface ICommentService
    {
        PaginatedList<GetCommentsDto> GetAll(int page, string text = "");
    }
}
