
using Lab1_ClientGUI.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab1_ClientGUI.Services
{
    public class EchoClientService
    {
        public event Action<string> LogChanged;
        private readonly MainSettings _settings;
        private Socket _socket;

        public EchoClientService(IOptions<MainSettings> options)
        {
            _settings = options.Value;
        }

        public void Connect()
        {
            LogChanged?.Invoke($"1Establishing connection with {_settings.Host}");
            IPAddress[] IPs = Dns.GetHostAddresses(_settings.Host);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            LogChanged?.Invoke($"Establishing connection with {_settings.Host}");
            //_socket.Connect(IPs[0], _settings.Port);
            //LogChanged?.Invoke($"Connected: {_socket.Connected}");

            //Thread threadReceive = new Thread(new ParameterizedThreadStart(Receive));
            //threadReceive.Start(_socket);
        }

        public void Send(string message)
        {
            _socket.Send(Encoding.ASCII.GetBytes(message));
            LogChanged?.Invoke($"Sent: {message}");
        }

        void Receive(object socketObj)
        {
            var socket = (Socket)socketObj;
            while (true)
            {
                byte[] buffer = new byte[1024];
                int result = socket.Receive(buffer);
                var message = Encoding.ASCII.GetString(buffer, 0, result);
                //_logger.LogInformation($"Message received: {message}");
                Console.Out.Flush();
            }
        }
    }
}
