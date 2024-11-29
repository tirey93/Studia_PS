using Lab1_Client.Services;
using Lab1_Client.Settings;
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
    .AddTransient<EchoClientService>()
    .BuildServiceProvider();

var echoClientService = serviceProvider.GetRequiredService<EchoClientService>();

echoClientService.Execute();