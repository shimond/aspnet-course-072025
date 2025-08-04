namespace MyStore.Api.Models.Dtos;

public interface IValidatable
{
}

public record AddProductRequest  : IValidatable
{
    public required string ProductName { get; init; }
    public decimal ProductPrice { get; init; }
    public string? Description { get; init; }
}
