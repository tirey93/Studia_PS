
using Lab1_Client.Settings;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using System.Net;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Lab1_Client.Services
{
    public class EchoClientService
    {
        private readonly MainSettings _settings;
        private readonly ILogger<EchoClientService> _logger;

        public EchoClientService(IOptions<MainSettings> options, ILogger<EchoClientService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public void Connect()
        {
            IPAddress[] IPs = Dns.GetHostAddresses(_settings.Host);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _logger.LogInformation("Establishing connection with {0}", _settings.Host);
            socket.Connect(IPs[0], _settings.Port);
            _logger.LogInformation($"Connected: {socket.Connected}");

            var message = "";
            do
            {
                Console.Out.Flush();
                Console.Write("Write a message: ");
                message = Console.ReadLine();

                socket.Send(Encoding.ASCII.GetBytes(message));
            } while (!string.IsNullOrEmpty(message));

        }
    }
}
