namespace Company.ProjectName.Infraestructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistsByNameAsync(string name) =>
        await _dbSet.AsNoTracking().AnyAsync(p => p.Name == name);

    public async Task<IEnumerable<Product>> GetActiveAsync() =>
        await _dbSet.AsNoTracking()
            .Where(p => p.Status == ProductStatus.Active)
            .ToListAsync();
}
