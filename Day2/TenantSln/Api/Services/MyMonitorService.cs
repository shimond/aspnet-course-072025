using Api.Model.Config;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    //if register as singletone than use IOptionsMonitor
    public class MyMonitorService (IOptionsMonitor<TenantConfig> tenantsOptions)
    {

        public async Task Start()
        {
            while (true)
            {
                await Task.Delay(10000);
                // tenantsOptions.CurrentValue

            }
        }
    }
}
