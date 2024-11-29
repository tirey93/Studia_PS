
using Lab1_Client.Settings;
using Microsoft.Extensions.Options;

namespace Lab1_Client.Services
{
    public class EchoClientService
    {
        private readonly MainSettings _settings;

        public EchoClientService(IOptions<MainSettings> options)
        {
            _settings = options.Value;
        }

        public void Execute()
        {
            Console.WriteLine(_settings.Field);

        }
    }
}
