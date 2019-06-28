using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.DTOs
{
    public class RolePostModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public static UserRole ToRole(RolePostModel role)
        {
            return new UserRole
            {
                Name = role.Name,
                Description = role.Description
            };
        }
    }
}
