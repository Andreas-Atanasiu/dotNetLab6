using Lab2.DTOs;
using Lab2.Models;
using Lab2.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface IUsersService
    {
        GetUserDto Authenticate(string username, string password);
        ErrorsCollection Register(PostUserDto registerInfo);
        User GetCurrentUser(HttpContext httpContext);
        IEnumerable<GetUserDto> GetAll();

        //Lab 5
        User GetUserById(int id);
        User UpdateUserNoRoleChange(int id, User user); //, User currentUser);
        User DeleteUser(int id); //, User currentUser);
    }

    public class UsersService : IUsersService
    {
        private ExpensesDbContext context;
        private readonly AppSettings appSettings;
        private IRegisterValidator registerValidator;

        public UsersService(ExpensesDbContext context, IRegisterValidator registerValidator, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings.Value;
            this.registerValidator = registerValidator;
        }

        public GetUserDto Authenticate(string username, string password)
        {
            var user = context.Users
                .SingleOrDefault(x => x.Username == username && x.Password == ComputeSha256Hash(password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.UserRole.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = new GetUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token),
                UserRole = user.UserRole
                
            };

            return result;
        }

        private String ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public ErrorsCollection Register(PostUserDto registerInfo)
        {

            var errors = registerValidator.Validate(registerInfo, context);
            if (errors != null)
            {
                return errors; 
            }

            context.Users.Add(new User
            {
                LastName = registerInfo.LastName,
                FirstName = registerInfo.FirstName,
                Password = ComputeSha256Hash(registerInfo.Password),
                Username = registerInfo.Username,
                UserRole = UserRole.Regular,
                DateAdded = DateTime.Now
               
            });

            context.SaveChanges();
            //return Authenticate(registerInfo.Username, registerInfo.Password);
            return null;
        }

        public User GetCurrentUser(HttpContext httpContext)
        {
            string username = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            //string accountType = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.AuthenticationMethod).Value;
            //return _context.Users.FirstOrDefault(u => u.Username == username && u.AccountType.ToString() == accountType);
            return context.Users.FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<GetUserDto> GetAll()
        {
            // return users without passwords
            return context.Users.Select(user => new GetUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Token = null,
                UserRole = user.UserRole

            });
        }

        public User GetUserById(int id)
        {
            return context.Users.AsNoTracking()
                .FirstOrDefault(u => u.Id == id);
        }

        //currentUser = userul logat
        //user        = userul existent, cu valori noi
        public User UpdateUserNoRoleChange(int id, User user) //, User currentUser)
        {
            
            User userToBeUpdated = GetUserById(id);

            user.Id = id;
            user.UserRole = userToBeUpdated.UserRole; //UserRole Update not permitted
            var userPassRecieved = ComputeSha256Hash(user.Password);

            if ((user.Password == "") || (userPassRecieved == userToBeUpdated.Password))
            {
                user.Password = userToBeUpdated.Password;
            }  else
            {
                user.Password = userPassRecieved;
            }

            user.DateAdded = DateTime.Now;

            context.Users.Update(user);
            context.SaveChanges();
            
            //don't return the password
            user.Password = null;
            return user;



        //int monthsDiff = DateTimeUtils.GetMonthDifference(currentUser.DateAdded, DateTime.Now);
        //
        //if (currentUser.UserRole == UserRole.UserManager && monthsDiff < 6)
        //{
        //    user.UserRole = userToBeUpdated.UserRole;
        //}

        user.Password = ComputeSha256Hash(userToBeUpdated.Password);
            user.DateAdded = DateTime.Now;
            context.Users.Update(user);
            context.SaveChanges();
            return user;

        }

        public User DeleteUser(int id)
        {
            var existing = context.Users.FirstOrDefault(user => user.Id == id);
            if (existing == null)
            {
                return null;
            }

            context.Users.Remove(existing);
            context.SaveChanges();

            return existing;
        }


    }
}
