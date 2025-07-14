using Api.Model.Config;

namespace Api.Contracts;

public interface ITenantContextAccessor
{
    Tenant CurrentTenant { get; }
    void SetCurrentTenant(Tenant tenant);
}
