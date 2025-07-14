namespace Api.Model.Config;

public record ServerInfoConfig
{
    public string LoggingFolder { get; init; } = "";
    public string[] AllowClients { get; init; } = Array.Empty<string>();
    public int DefaultTimeOut { get; init; }
    public bool AllowHttps { get; init; }
}



