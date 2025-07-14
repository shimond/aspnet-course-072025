using Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(ITenantContextAccessor contextAccessor) : ControllerBase
{

    [HttpGet("test")]
    public ActionResult Test()
    {
        var current = contextAccessor.CurrentTenant;
        return Ok(current);

    }

}
