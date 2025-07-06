using Api.Contracts;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController(IGeneratorService generatorService) : ControllerBase
{
    //private readonly IGeneratorService _generatorService;

    //public ValuesController(IGeneratorService  generatorService)
    //{
    //    this._generatorService = generatorService;
    //}

    [HttpGet("generate/{len}")]
    public string GetRandomString(int len)
    {
        
        string randomString = generatorService.GenerateString(len);
        return randomString;
    }
    
    [HttpGet("generateInt/{min}/{max}")]
    public int GetRandomNumber(int min, int max)
    {
        var randomValue = generatorService.GenerateInt(min, max);
        return randomValue;
    }


    [HttpGet]
    [OutputCache(Duration =25)]
    public string Get()
    {
        Thread.Sleep(5000);
        return "Hello World!";
    }


    [HttpPost]
    public string Test()
    {
        return "Hello World! from post http";
    }

}
