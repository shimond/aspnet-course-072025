using Api.Contracts;
using Api.Model.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class TenantValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IOptions - value on startup
        // IOptionsSnapshot - value on runtime
        // IOptionsMonitor - 
        public Task Invoke(HttpContext httpContext, IOptionsSnapshot<TenantConfig> tenantsOptions, ITenantContextAccessor tenantContextAccessor)
        {
            var tenantId = httpContext.Request.Headers["X-Tenant-ID"].ToString();
            if (string.IsNullOrEmpty(tenantId))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return httpContext.Response.WriteAsync("Tenant ID is required.");
            }
            var allTenants = tenantsOptions.Value.Tenants;
            var currentTenant = allTenants.FirstOrDefault(t => t.Name.Equals(tenantId, StringComparison.OrdinalIgnoreCase));
            if(currentTenant is null)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return httpContext.Response.WriteAsync($"Tenant '{tenantId}' not found.");
            }

            tenantContextAccessor.SetCurrentTenant(currentTenant);

            return _next(httpContext);
        }
    }

    public static class TenantValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantValidationMiddleware>();
        }
    }
}
