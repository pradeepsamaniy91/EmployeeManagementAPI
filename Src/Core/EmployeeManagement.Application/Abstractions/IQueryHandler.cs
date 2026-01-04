using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Application.Abstractions
{
    public interface IQueryHandler<TQuery,TResponse>:IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
    {
    }

}
