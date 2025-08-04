var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("MyStoreDBServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var sqlDatabase = sql.AddDatabase("MyStoreDB");
    
var seq = builder.AddSeq("seq")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();


var myStoreApi = builder.AddProject<Projects.MyStore_Api>("mystore-api")
    .WithReference(seq)
    .WaitFor(seq)
    .WithReference(sqlDatabase)
    .WaitFor(sqlDatabase);
 

builder.AddProject<Projects.BFF>("bff")
        .WaitFor(myStoreApi)
        .WithReference(myStoreApi);


builder.Build().Run();
