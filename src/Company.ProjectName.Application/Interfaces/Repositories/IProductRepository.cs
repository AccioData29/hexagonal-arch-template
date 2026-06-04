namespace Company.ProjectName.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<IEnumerable<Product>> GetActiveAsync();
}
