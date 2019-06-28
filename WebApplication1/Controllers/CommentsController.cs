using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.DTOs;
using Lab2.Models;
using Lab2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService commentService; 

        public CommentsController(ICommentService service)
        {
            this.commentService = service;
        }

        [HttpGet]
        public PaginatedList<GetCommentsDto> Get([FromQuery]string filterString, [FromQuery]int page = 1)
        {
            page = Math.Max(page, 1);
            return commentService.GetAll(page, filterString);
        }

        // [HttpGet]
        // public IEnumerable<GetCommentsDto> GetComments(string text = "")
        // {
        //     return commentService.GetAll(text);
        // }


    }
}