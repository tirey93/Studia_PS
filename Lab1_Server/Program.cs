using Lab1_Server.Services;
using Lab1_Server.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

var serviceProvider = new ServiceCollection()
    .AddLogging(x =>
    {
        x.AddConfiguration(configuration.GetSection("Logging"));
        x.AddConsole();
    })
    .Configure<MainSettings>(configuration.GetSection(nameof(MainSettings)))
    .AddTransient<EchoServerService>()
    .BuildServiceProvider();

var echoServerService = serviceProvider.GetRequiredService<EchoServerService>();

echoServerService.Execute();