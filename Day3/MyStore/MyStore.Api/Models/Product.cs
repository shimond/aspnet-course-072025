using System.ComponentModel.DataAnnotations;

namespace MyStore.Api.Models;

public record Product
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
}
