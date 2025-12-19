var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApplication>("webapplication");

builder.Build().Run();
