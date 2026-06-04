namespace Company.ProjectName.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id)
            ?? throw new NotFoundException($"Product {id} not found.");
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        if (await _unitOfWork.Products.ExistsByNameAsync(request.Name))
            throw new DuplicateException($"A product with name '{request.Name}' already exists.");

        var product = _mapper.Map<Product>(request);
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> UpdateAsync(Guid id, UpdateProductRequest request)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id)
            ?? throw new NotFoundException($"Product {id} not found.");

        _mapper.Map(request, product);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id)
            ?? throw new NotFoundException($"Product {id} not found.");

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveChangesAsync();
    }
}
