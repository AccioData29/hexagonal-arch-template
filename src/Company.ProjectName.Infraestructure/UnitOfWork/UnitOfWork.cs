namespace Company.ProjectName.Infraestructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IProductRepository? _products;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository Products =>
        _products ??= new ProductRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("unique") == true
                                        || ex.InnerException?.Message.Contains("duplicate") == true)
        {
            throw new UniqueConstraintException("A record with the same key already exists.");
        }
    }

    public void Dispose() => _context.Dispose();
}
