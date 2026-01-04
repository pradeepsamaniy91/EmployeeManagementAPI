using System.Diagnostics.Contracts;
using System.Net;

namespace EmployeeManagement.Domain.ValueObjects;

using System.Net;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }
    public HttpStatusCode HttpStatusCode { get; }

    protected internal Result(bool isSuccess, string error, HttpStatusCode httpStatusCode)
    {
        // Validation logic
        if (isSuccess && !string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("Success result cannot have an error.");
        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("Failure result must have an error.");

        IsSuccess = isSuccess;
        Error = error;
        HttpStatusCode = httpStatusCode;
    }

    // Static Factory Methods
    public static Result Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        => new(true, string.Empty, httpStatusCode);

    public static Result<T> Success<T>(T value, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        => new(value, true, string.Empty, httpStatusCode);

    public static Result Failure(string error, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        => new(false, error, httpStatusCode);

    public static Result<T> Failure<T>(string error, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        => new(default, false, error, httpStatusCode);

    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>("Value cannot be null", HttpStatusCode.NotFound);
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T? value, bool isSuccess, string error, HttpStatusCode httpStatusCode)
        : base(isSuccess, error, httpStatusCode)
        => _value = value;
    public T Value => IsSuccess ?
        _value! :
        throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    // Corrected Implicit Operator
    public static implicit operator Result<T>(T? value) => Create(value);
}

