namespace Api.Model.Config;

public class ServerInfoConfig
{
    public string LoggingFolder { get; set; } = "";
    public string[] AllowClients { get; set; } = [];
    public int DefaultTimeOut { get; set; }
    public bool AllowHttps { get; set; }
}

