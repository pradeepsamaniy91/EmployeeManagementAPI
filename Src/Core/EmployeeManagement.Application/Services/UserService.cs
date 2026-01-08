using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetUserById(long empId, CancellationToken cancellationToken)
        {
            // Use FirstOrDefaultAsync and pass the cancellationToken
            return await _context.Users
                .FirstOrDefaultAsync(u => u.EmpId == empId, cancellationToken);
        }


        Task<List<User?>> IUserRepository.GetUsersByIDAndPasswordAsync(string userName, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
