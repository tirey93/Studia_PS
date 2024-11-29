
using Lab1_Server.Settings;
using Microsoft.Extensions.Options;

namespace Lab1_Server.Services
{
    public class EchoServerService
    {
        private readonly MainSettings _settings;

        public EchoServerService(IOptions<MainSettings> options)
        {
            _settings = options.Value;
        }

        public void Execute()
        {
            Console.WriteLine(_settings.Field);
        }
    }
}
