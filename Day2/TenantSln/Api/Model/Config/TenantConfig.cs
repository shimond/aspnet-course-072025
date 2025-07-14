namespace Api.Model.Config;

public record TenantConfig
{
    public string DefaultTenant { get; init; } = "";
    public int DefaultMaxUsers { get; init; }
    public Tenant[] Tenants { get; init; } = [];
}

public class Tenant
{
    public required string Name { get; init; }
    public bool EnableCheckout { get; init; }
    public int MaxItemsPerUser { get; init; }
    public string[] AllowedItemCategories { get; init; } = [];
    public Features? Features { get; init; }
}

public class Features
{
    public bool AdvancedSearch { get; init; }
    public bool ExportToExcel { get; init; }
    public bool SpecialReport { get; init; }
}

