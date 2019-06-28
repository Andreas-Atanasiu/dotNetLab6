using Lab2.DTOs;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Validators
{
    public interface IRoleValidator
    {
        ErrorsCollection ValidateRole(RoleIdModel roleIdModel, ExpensesDbContext context);
    }
    public class RoleValidator : IRoleValidator
    {
        public ErrorsCollection ValidateRole(RoleIdModel roleIdModel, ExpensesDbContext context)
        {
            ErrorsCollection errors = new ErrorsCollection
            {
                Entity = nameof(RolePostModel)
            };

            UserRole role = context.UserRoles.FirstOrDefault(r => r.Id == roleIdModel.RoleId);

            if (role == null)
            {
                errors.ErrorMessages.Add($"The role {roleIdModel.RoleId} does not exist!");
            }
            return null;
        }
    }
}
