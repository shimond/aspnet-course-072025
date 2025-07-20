using System.ComponentModel.DataAnnotations;

namespace MyStore.Api.Models;

public record Product
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
}

// 1. Security
// 2. Validation
// 3. Mapping

// BFF - Backend for Frontend
