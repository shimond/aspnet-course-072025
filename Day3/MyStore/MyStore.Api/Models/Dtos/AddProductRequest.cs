namespace MyStore.Api.Models.Dtos
{
    public record AddProductRequest
    {
        public required string Name { get; init; }
        public decimal Price { get; init; }
        public string? Description { get; init; }
    }
}
