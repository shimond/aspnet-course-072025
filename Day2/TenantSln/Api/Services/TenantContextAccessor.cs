using Api.Contracts;
using Api.Model.Config;

namespace Api.Services;


public class TenantContextAccessor : ITenantContextAccessor
{
    private Tenant? _currentTenant;
    public Tenant CurrentTenant
    {
        get => _currentTenant ?? throw new InvalidOperationException("Current tenant is not set.");
    }

    public void SetCurrentTenant(Tenant tenant)
    {
        _currentTenant = tenant;
    }

}
