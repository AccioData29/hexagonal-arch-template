namespace Company.ProjectName.Tests.Helpers;

public static class UnitOfWorkHelper
{
    public static UnitOfWork Create(AppDbContext context) => new(context);
}
