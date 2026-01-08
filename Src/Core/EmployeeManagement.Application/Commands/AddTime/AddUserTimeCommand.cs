using EmployeeManagement.Application.Dto.AddTime;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Commands.AddTime;

public class AddUserTimeCommand:IRequest<Result>
{
    public AddUserTimeCommand(AddUserTimeDto addUserTimeDto)
    {
        AddUserTimeDto = addUserTimeDto;
    }
    public AddUserTimeDto AddUserTimeDto { get; set; }
}
