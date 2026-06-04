namespace Company.ProjectName.Application.Exceptions;

public class UniqueConstraintException : BaseException
{
    public UniqueConstraintException(string message) : base(message, 409) { }
}
