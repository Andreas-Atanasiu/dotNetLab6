using Lab2.Models;
using Lab2.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface IRoleService
    {
        IEnumerable<RolePostModel> GetAll();
        UserRole GetById(int id);
        UserRole Create(RolePostModel role);
        UserRole Upsert(int id, UserRole role);
        UserRole Delete(int id);
    }
    public class RoleService : IRoleService
    {
        private ExpensesDbContext context;

        public RoleService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<RolePostModel> GetAll()
        {
            IQueryable<RolePostModel> result = context
                .UserRoles.Select(r => new RolePostModel
                {
                    Name = r.Name,
                    Description = r.Description
                });
            return result;
        }

        public UserRole GetById(int id)
        {
            return context.UserRoles.FirstOrDefault(r => r.Id == id);
        }

        public UserRole Create(RolePostModel role)
        {
            UserRole roleToAdd = RolePostModel.ToRole(role);
            context.UserRoles.Add(roleToAdd);
            context.SaveChanges();
            return roleToAdd;
        }

        public UserRole Upsert(int id, UserRole role)
        {
            var existing = context.UserRoles.AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
            if (existing == null)
            {
                context.UserRoles.Add(role);
                context.SaveChanges();
                return role;
            }

            //existing.Name = role.Name;
            //existing.Description = role.Description;

            role.Id = id;
            context.UserRoles.Update(role);

            //context.Entry(existing).State = EntityState.Detached;

            context.SaveChanges();

            return existing;
        }

        public UserRole Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null)
            {
                return null;
            }
            context.UserRoles.Remove(existing);
            context.SaveChanges();
            return existing;
        }

    }
}
