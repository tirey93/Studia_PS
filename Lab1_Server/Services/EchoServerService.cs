
using Lab1_Server.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab1_Server.Services
{
    public class EchoServerService
    {
        private readonly MainSettings _settings;
        private readonly ILogger<EchoServerService> _logger;

        public EchoServerService(IOptions<MainSettings> options, ILogger<EchoServerService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public void Connect()
        {
            IPAddress[] IPs = Dns.GetHostAddresses(_settings.Host);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(new IPEndPoint(IPAddress.Parse(_settings.Host), _settings.Port));
            socket.Listen(_settings.Backlog);
            _logger.LogInformation("Server open on port: {0}", _settings.Port);
            Socket cli = socket.Accept();
            _logger.LogInformation("Connected with {0}", cli.RemoteEndPoint.ToString());


            while (true)
            {
                byte[] buffer = new byte[1024];
                int result = cli.Receive(buffer);
                var message = Encoding.ASCII.GetString(buffer, 0, result);
                _logger.LogInformation($"Message received: {message}");

            }
        }

    }
}
