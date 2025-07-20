namespace MyStore.Api.Models.Dtos
{
    public record AddProductRequest
    {
        public required string NewProductName { get; init; }
        public decimal NewProductPrice { get; init; }
        public string? Description { get; init; }
    }
}
