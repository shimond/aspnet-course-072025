using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesManagerController : ControllerBase
    {

        [HttpGet("test")]
        public string Test()
        {
            return "Hello World! from FilesManagerController";
        }

    }
}
