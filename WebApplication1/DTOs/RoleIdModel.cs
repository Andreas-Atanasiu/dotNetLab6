using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.DTOs
{
    public class RoleIdModel
    {
        public int RoleId { get; set; }

        public static UserRole ToRole(RoleIdModel roleModel)
        {
            return new UserRole
            {
                Id = roleModel.RoleId
            };
        }
    }
}
