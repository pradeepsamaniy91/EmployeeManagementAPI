using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync();
        Task<List<User?>> GetUsersByIDAndPasswordAsync(string userName,string password,CancellationToken cancellationToken);
        Task<User?> GetUserById(long empId,CancellationToken cancellationToken);
        Task<User?> CreateUser(User user,CancellationToken cancellationToken);
    }
}
