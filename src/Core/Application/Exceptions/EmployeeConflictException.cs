using Application.Exceptions.Base;

namespace Application.Exceptions
{
    public sealed class EmployeeConflictException(string value)
        : BaseException($"There's already a employee registered with the property {value}")
    {
    }
}
