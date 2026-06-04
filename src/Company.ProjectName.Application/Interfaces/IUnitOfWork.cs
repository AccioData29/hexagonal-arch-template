namespace Company.ProjectName.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
