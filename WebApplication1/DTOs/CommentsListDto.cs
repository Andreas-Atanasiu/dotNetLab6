using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.DTOs
{
    public class CommentsListDto
    {
        public IEnumerable<GetCommentsDto> CommentsList { get; set; } = new List<GetCommentsDto>();
    }
}
