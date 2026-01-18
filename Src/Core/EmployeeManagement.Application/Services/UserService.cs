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

       

        public async Task<User?> GetUserByEmailId(string emailId, CancellationToken cancellationToken)
        {
            // Use FirstOrDefaultAsync and pass the cancellationToken
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == emailId, cancellationToken);
        }
        public async Task<User?> GetUserById(long userId, CancellationToken cancellationToken)
        {
            // Use FirstOrDefaultAsync and pass the cancellationToken
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
        }
        public async Task<User?> GetUserByEmployeeId(long employeeId, CancellationToken cancellationToken)
        {
            // Use FirstOrDefaultAsync and pass the cancellationToken
            return await _context.Users
                .FirstOrDefaultAsync(u => u.EmpId == employeeId, cancellationToken);
        }       

    }
}
