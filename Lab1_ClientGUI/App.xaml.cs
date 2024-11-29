using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Lab1_ClientGUI.Settings;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Lab1_ClientGUI.Services;

namespace Lab1_ClientGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            services.Configure<MainSettings>(configuration.GetSection(nameof(MainSettings)));
            services.AddSingleton<MainWindow>();
            services.AddSingleton<EchoClientService>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }

}
