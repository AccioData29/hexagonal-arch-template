namespace Company.ProjectName.Application.Dto.Requests;

public class UpdateProductRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ProductStatus Status { get; set; }
}
