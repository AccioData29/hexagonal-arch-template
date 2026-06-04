namespace Company.ProjectName.Application.Interfaces.Services;

public interface IProductService
{
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<ProductResponse> CreateAsync(CreateProductRequest request);
    Task<ProductResponse> UpdateAsync(Guid id, UpdateProductRequest request);
    Task DeleteAsync(Guid id);
}
