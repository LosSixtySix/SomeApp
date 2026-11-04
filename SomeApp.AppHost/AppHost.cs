var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SomeApp>("someapp");

builder.AddProject<Projects.SomeApi>("someapi");

builder.Build().Run();
