using Lab2.Models;
using Lab2.Services;
using Lab2.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;

namespace Tests
{
    class Tests
    {
        private IRoleValidator roleValidator;

        private IOptions<AppSettings> config;
        [SetUp]
        public void Setup()
        {  
            config = Options.Create(new AppSettings
            {
                Secret = "adlkfadlkasfaskldffalaksfaDFLKAjdflkadjfaldkfjsd"
            });

            roleValidator = new RoleValidator();

        }

        [Test]
        public void ValidRegisterShouldCreateANewUser()
        {
            var options = new DbContextOptionsBuilder<ExpensesDbContext>()
                .UseInMemoryDatabase(databaseName: "ValidRegisterShouldCreateANewUser")
                .Options;


            using (var context = new ExpensesDbContext(options))
            {
                var usersService = new UsersService(context, null, config, roleValidator);

               var added = new Lab2.DTOs.PostUserDto
                {
                    FirstName = "Andreas",
                    LastName = "LastName",
                    Username = "aa1234",
                    Password = "123456"
                };

                var result = usersService.Register(added);


                Assert.IsNotNull(result);
                //ssert.AreEqual(added.Username, result.Username);
            }
        }
    }
}