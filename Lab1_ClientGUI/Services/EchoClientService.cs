
using Lab1_ClientGUI.Settings;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Lab1_ClientGUI.Services
{
    public class EchoClientService
    {
        private readonly MainSettings _settings;

        public EchoClientService(IOptions<MainSettings> options)
        {
            _settings = options.Value;
        }

        public void Connect()
        {
            IPAddress[] IPs = Dns.GetHostAddresses(_settings.Host);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //_logger.LogInformation("Establishing connection with {0}", _settings.Host);
            socket.Connect(IPs[0], _settings.Port);
            //_logger.LogInformation($"Connected: {socket.Connected}");
            Console.Out.Flush();

            Thread threadReceive = new Thread(new ParameterizedThreadStart(Receive));
            threadReceive.Start(socket);

            var message = "";
            do
            {
                Console.Write("Write a message: ");
                message = Console.ReadLine();

                socket.Send(Encoding.ASCII.GetBytes(message));
            } while (!string.IsNullOrEmpty(message));

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
