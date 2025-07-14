using Api.Contracts;
using Api.Model.Config;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Options;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController(IConfiguration configuration, IOptions<ServerInfoConfig> serverOptions) : ControllerBase
{

    [HttpGet("complex")]
    public ActionResult GetValueFromConfigComplex()
    {
        var value = serverOptions.Value;
        //value.AllowHttps = true;
        return Ok(value);
    }


    [HttpGet("Conf")]   
    
    public ActionResult<string> GetValueFromConfig()
    {
        //1.appsettings.json
        //2.appsettings.{EnvName}.json
        //3.User secrets
        //4.Env variables
        //5.Command line arguments
        var logFolder = configuration["ServerInfo:LoggingFolder"];
        var DELAY_AFTER_DEPLOY_COMPLETE_SECONDS = configuration["DELAY_AFTER_DEPLOY_COMPLETE_SECONDS"];
        var valueForTest = configuration["valueForTest"];
        var ShimonValue  = configuration["ShimonValue"];
        //var c2 = configuration["Path"];
        return Ok(new string [] { 
            logFolder, 
            valueForTest, 
            ShimonValue, 
            DELAY_AFTER_DEPLOY_COMPLETE_SECONDS
        });
    }

}



// ConnectionString
// Keys
// Avoid hardcoding sensitive information in the codebase.