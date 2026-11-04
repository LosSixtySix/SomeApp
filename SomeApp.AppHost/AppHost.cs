var builder = DistributedApplication.CreateBuilder(args);

var mauiApp = builder.AddMauiProject("someapp","../SomeApp/SomeApp.csproj");

var api = builder.AddProject<Projects.SomeApi>("someapi");

mauiApp.AddWindowsDevice()
       .WithReference(api);

builder.Build().Run();
