using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Expense
    {
        //[Key()]
        public int Id { get; set; }

        //[Required]
        public string Description { get; set; }

        //[Required]
        public int Sum { get; set; }

        public string Location { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        public string Currency { get; set; }

        //[Required]
        //[EnumDataType(typeof(TypeEnum))]
        public TypeEnum Type { get; set; }

        public User Owner { get; set; }

        public List<Comment> Comments { get; set; }

    }
}

