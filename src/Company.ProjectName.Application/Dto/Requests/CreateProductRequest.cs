namespace Company.ProjectName.Application.Dto.Requests;

public class CreateProductRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
