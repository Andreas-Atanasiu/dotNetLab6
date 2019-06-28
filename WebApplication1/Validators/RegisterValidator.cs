using Lab2.DTOs;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Validators
{

    public interface IRegisterValidator
    {
        ErrorsCollection Validate(PostUserDto postUserDto, ExpensesDbContext expensesDbContex);
    }
    public class RegisterValidator : IRegisterValidator
    {

        public ErrorsCollection Validate(PostUserDto postUserDto, ExpensesDbContext expensesDbContext)
        {
            ErrorsCollection errorsCollection = new ErrorsCollection { Entity = nameof(postUserDto) };

            User existing = expensesDbContext.Users.FirstOrDefault(u => u.Username == postUserDto.Username);

            if (existing != null)
            {
                errorsCollection.ErrorMessages.Add($"Username {postUserDto.Username} is already used!");

            }


            if (postUserDto.Password.Length < 6)
            {
                errorsCollection.ErrorMessages.Add("The password has to have > 5 chars");

            }

            int numberOfDigits = 0;
            foreach (char c in postUserDto.Password)
            {
                if (c >= '0' && c <= '9')
                {
                    numberOfDigits++;
                }
            }
            if (numberOfDigits < 2)
            {
                errorsCollection.ErrorMessages.Add("The password must contain at least 2 digits");

            }

            if (errorsCollection.ErrorMessages.Count > 0)
            {
                return errorsCollection;
            }
            return null;
        }
    }
}
