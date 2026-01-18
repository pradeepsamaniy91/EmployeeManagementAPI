using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(long userId,CancellationToken cancellationToken);
        Task<User?> GetUserByEmployeeId(long employeeId,CancellationToken cancellationToken);
        Task<User?> GetUserByEmailId(string emailId,CancellationToken cancellationToken);
        Task<User?> CreateUser(User user,CancellationToken cancellationToken);
    }
}
