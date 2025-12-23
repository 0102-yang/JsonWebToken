var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.JsonWebToken_Web>("web");

builder.Build().Run();
