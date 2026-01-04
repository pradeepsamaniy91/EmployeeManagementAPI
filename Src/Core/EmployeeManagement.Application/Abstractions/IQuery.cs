using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Abstractions;

public interface IQuery<T>:IRequest<Result<T>>
{
}
