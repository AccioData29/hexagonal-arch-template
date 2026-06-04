namespace Company.ProjectName.Application.Exceptions;

public class ValidationException : BaseException
{
    public ValidationException(string message, List<string> errors)
        : base(message, 422, errors) { }
}
