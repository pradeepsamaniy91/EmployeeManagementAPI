using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Services
{
    public class UserService : IUserRepository
    {
        private readonly EmployeeManagementContext _context;
        public UserService(EmployeeManagementContext context) 
        { 
            _context = context;
        }

        async Task<User?> IUserRepository.CreateUser(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        Task<User?> IUserRepository.GetUserAsync()
        {
            throw new NotImplementedException();
        }

        async Task<User?> IUserRepository.GetUserById(long empId, CancellationToken cancellationToken)
        {
            return  _context.Users.Where(u=>u.EmpId== empId).FirstOrDefault();
        }

        Task<List<User?>> IUserRepository.GetUsersByIDAndPasswordAsync(string userName, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
