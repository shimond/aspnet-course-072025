namespace MyStore.Api.Models.Dtos;

public record ProductDTO
{
    public int Id { get; init; }
    public required string ProductName { get; init; }
    public decimal ProductPrice { get; init; }
    public string? Description { get; init; }
}
