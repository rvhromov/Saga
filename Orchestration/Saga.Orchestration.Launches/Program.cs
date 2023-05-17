using Microsoft.Extensions.Hosting;
using Saga.Orchestration.Launches;

using IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostBuilder, services) => services.AddServices(hostBuilder.Configuration))
    .Build();

await host.RunAsync();