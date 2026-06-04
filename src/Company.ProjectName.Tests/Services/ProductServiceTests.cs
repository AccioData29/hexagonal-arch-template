namespace Company.ProjectName.Tests.Services;

public class ProductServiceTests
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ProductService _sut;

    public ProductServiceTests()
    {
        _context = DbContextHelper.CreateInMemoryContext();
        _unitOfWork = UnitOfWorkHelper.Create(_context);
        _mapper = MapperHelper.Create();
        _sut = new ProductService(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProduct_WhenExists()
    {
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            Name = "Test Product",
            Price = 10.00m,
            Stock = 5,
            Status = ProductStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var result = await _sut.GetByIdAsync(product.ProductId);

        result.Should().NotBeNull();
        result.Name.Should().Be("Test Product");
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsNotFoundException_WhenNotExists()
    {
        var act = () => _sut.GetByIdAsync(Guid.NewGuid());

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task CreateAsync_CreatesProduct_WhenNameIsUnique()
    {
        var request = new CreateProductRequest
        {
            Name = "New Product",
            Price = 25.00m,
            Stock = 10
        };

        var result = await _sut.CreateAsync(request);

        result.Should().NotBeNull();
        result.Name.Should().Be("New Product");
        result.Status.Should().Be(ProductStatus.Active);
    }

    [Fact]
    public async Task CreateAsync_ThrowsDuplicateException_WhenNameExists()
    {
        var product = new Product
        {
            ProductId = Guid.NewGuid(),
            Name = "Existing",
            Price = 10.00m,
            Stock = 5,
            Status = ProductStatus.Active,
            CreatedAt = DateTime.UtcNow
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var act = () => _sut.CreateAsync(new CreateProductRequest { Name = "Existing", Price = 5m });

        await act.Should().ThrowAsync<DuplicateException>();
    }

    [Fact]
    public async Task DeleteAsync_ThrowsNotFoundException_WhenNotExists()
    {
        var act = () => _sut.DeleteAsync(Guid.NewGuid());

        await act.Should().ThrowAsync<NotFoundException>();
    }
}
